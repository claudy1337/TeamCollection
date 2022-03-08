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
using MongoDB.Driver.Linq;



namespace TeamCollection
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] teamList = new string[5] { "One", "Two", "Three", "Four", "Five" };
        
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



        private async void BUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("TeamCollection");
            var collection = database.GetCollection<BsonDocument>("User");
            var result = await collection.ReplaceOneAsync(new BsonDocument("Login", TBLogin.Text),
                new BsonDocument
                {
                    {"Login", TBLogin.Text },
                    {"Name",TBName.Text},
                    {"LastName", TBLastName.Text},
                    {"Number", TBNumber.Text}
                    
                });
            var people = await collection.Find(new BsonDocument()).ToListAsync();
            Refresh();
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
                

            Refresh();
        }

        private void BRandomTeam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> teams = User.GetLoginList();

                Random rnd = new Random();
                for (int i = 0; i < teamList.Length; i++)
                {
                    teamList[i] = teams[rnd.Next(0, teams.Count)];
                    teams.Remove(teamList[i]);
                }
                TeamateOne.Text = teamList[0];
                TeamateTwo.Text = teamList[1];
                TeamateThree.Text = teamList[2];
                TeamateFour.Text = teamList[3];
                TeamateFive.Text = teamList[4];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        int count = 0;
        private async void BUserAddTeam_Click(object sender, RoutedEventArgs e)
        {
            count++;
            string[] teamUserList = new string[5] { "One", "Two", "Three", "Four", "Five" };
            try
            {

                for (int i = 0; i < count; i++)
                {
                    if (listLogin.SelectedIndex != -1)
                    {
                        teamUserList[i] = listLogin.SelectedItem.ToString();
                        switch (count)
                        {
                            case 1:
                                TeamateOne.Text = teamUserList[i];
                                break;
                            case 2:
                                TeamateTwo.Text = teamUserList[i];
                                break;
                            case 3:
                                TeamateThree.Text = teamUserList[i];
                                break;
                            case 4:
                                TeamateFour.Text = teamUserList[i];
                                break;
                            case 5:
                                TeamateFive.Text = teamUserList[i];
                                break;
                            default:
                                MessageBox.Show("фул тима или нехватает учатсников");
                                count = 0;
                                RefreshTeam();
                                break;
                        }
                    }
                    else
                    {

                    }


                    
                }

                 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            //teamUserList[3] = User.GetUser(listLogin.SelectedItem.ToString()).Login;
            //teamUserList[4] = User.GetUser(listLogin.SelectedItem.ToString()).Login;

        }

        private void BClearTeam_Click(object sender, RoutedEventArgs e)
        {
            RefreshTeam();
            Refresh();
        }
        public void RefreshTeam()
        {
            TBTeamName.Text = "";
            TeamateOne.Text = "";
            TeamateTwo.Text = "";
            TeamateThree.Text = "";
            TeamateFour.Text = "";
            TeamateFive.Text = "";
        }
    }

}
