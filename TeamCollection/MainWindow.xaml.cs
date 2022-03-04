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


namespace TeamCollection
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
        }


        private async void BSaveTeam_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("testFurnitured");
            var collection = database.GetCollection<BsonDocument>("User");
            var filter = new BsonDocument();
            var people = await collection.Find(filter).ToListAsync();
            foreach (var doc in people)
            {
                DGProducts.ItemsSource = database.GetCollection<BsonDocument>("User").ToString();

            }



        }

        private async void DGProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("testFurnitured");
            var collection = database.GetCollection<BsonDocument>("User");
            var filter = new BsonDocument();
            var people = await collection.Find(filter).ToListAsync();
            foreach (var doc in people)
            {
                DGProducts.ItemsSource = doc.ToString();

            }
        }
    }

}
