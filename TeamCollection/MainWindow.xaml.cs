using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MongoDB.Bson;
using MongoDB.Driver;
using TeamCollection.DB;
using MongoDB.Bson.Serialization.Attributes;



namespace TeamCollection
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        User user = new User("Name", "Login", "LastName", "Number", false);
        public MainWindow()
        {
            InitializeComponent();
            Refresh();

        }

        private async void BAddUser_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("TeamCollection");
            var collection = database.GetCollection<BsonDocument>("User");

            BsonDocument person1 = new BsonDocument
            {
                {"Login", TBULogin.Text},
                {"Name", TBUName.Text},
                {"LastName", TBULastName.Text },
                {"Number", TBUNumber.Text},
                {"HasTeam", false }
            };

            await collection.InsertManyAsync(new[] { person1 });
            Refresh();
        }

        private void listLogin_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private async void listLogin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listLogin.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                TBLogin.Text = User.GetUser(listLogin.SelectedItem.ToString()).Login;
                TBName.Text = User.GetUser(listLogin.SelectedItem.ToString()).Name;
                TBNumber.Text = User.GetUser(listLogin.SelectedItem.ToString()).Number;
                TBLastName.Text = User.GetUser(listLogin.SelectedItem.ToString()).LastName;
            }
        }



        private void BUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            
        }


        public void Refresh()
        {
            listLogin.ItemsSource = null;
            listLogin.ItemsSource = User.GetLoginList();
            TBLogin.Text = "";
            TBLastName.Text = "";
            TBNumber.Text = "";
            TBName.Text = "";
            TBULastName.Text = "";
            TBULogin.Text = "";
            TBUName.Text = "";
            TBUNumber.Text = "";
        }

        private void BClear_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("TeamCollection");
            var collection = database.GetCollection<User>("User");

            var filter = Builders<User>.Filter.Eq("Login", listLogin.SelectedItem);
            await collection.DeleteOneAsync(filter);

            var people = await collection.Find(new BsonDocument()).ToListAsync();
            foreach (var p in people)
                MessageBox.Show($"Deleted user: {TBLogin.Text}");

            Refresh();
        }
    }

}
