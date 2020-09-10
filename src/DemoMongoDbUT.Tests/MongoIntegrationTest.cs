using DemoMongoDbUT.Infrastructures.Contexts;
using DemoMongoDbUT.Models;
using Mongo2Go;
using MongoDB.Driver;

namespace DemoMongoDbUT.Tests
{
    public class MongoIntegrationTest
    {
        internal static MongoDbRunner _runner;
        internal static IMessageDataContext _context;
        internal static IMongoCollection<Message> _collection;

        internal static void CreateConnection()
        {
            _runner = MongoDbRunner.Start();

            MongoClient client = new MongoClient(_runner.ConnectionString);
            IMongoDatabase database = client.GetDatabase("IntegrationTest");
            _collection = database.GetCollection<Message>("TestCollection");
        }
    }
}
