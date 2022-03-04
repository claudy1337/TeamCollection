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
    }
}
