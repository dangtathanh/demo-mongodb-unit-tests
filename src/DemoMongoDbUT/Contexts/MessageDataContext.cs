using MongoDB.Driver;
using DemoMongoDbUT.Models;
using DemoMongoDbUT.Infrastructures.Contexts;

namespace DemoMongoDbUT.Contexts
{
    public class MessageDataContext : IMessageDataContext
    {
        private readonly IMongoDatabase _database = null;

        public MessageDataContext(string connectionString)
        {
            var client = new MongoClient(connectionString);

            if (client != null)
            {
                _database = client.GetDatabase("SportPartner-MessageDB");
            }
        }

        public IMongoCollection<Message> Messages
        {
            get
            {
                return _database.GetCollection<Message>("MessageDb");
            }
        }
    }
}
