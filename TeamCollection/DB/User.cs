using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamCollection.DB
{
    class User
    {
        public User(string login,string name,string lastname, string number, bool hasTeam)
        {
            Name = name;
            Login = login;
            LastName = lastname;
            Number = number;
            HasTeam = hasTeam;
           
        }
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public bool HasTeam { get; set; }

        

        public static List<string> GetLoginList()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("TeamCollection");
            var collection = database.GetCollection<User>("User");
            var listUsersFromDB = collection.Find(x => true).ToList();
            List<string> listToReturn = new List<string>();
            foreach (var item in listUsersFromDB)
            {
                listToReturn.Add(item.Login);
            }
            return listToReturn;
        }

        
        public static User GetUser(string name)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("TeamCollection");
            var collection = database.GetCollection<User>("User");
            var foundedUser = collection.Find(x => x.Login == name).FirstOrDefault();
            return foundedUser;
        }
       

    }
}
