using MongoDB.Driver;

namespace TeamCollection.DB
{
    public class DbConnection
    {
        private static string _connectionString = "mongodb://localhost:27017";
        private static MongoClient _client = new MongoClient(_connectionString);
        IMongoClient MongoClient = (IMongoClient)_client.GetDatabase("TeamCollection");
    }
}
