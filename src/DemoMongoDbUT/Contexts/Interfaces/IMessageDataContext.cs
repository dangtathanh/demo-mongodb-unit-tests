using MongoDB.Driver;
using DemoMongoDbUT.Models;

namespace DemoMongoDbUT.Infrastructures.Contexts
{
    public interface IMessageDataContext
    {
        IMongoCollection<Message> Messages { get; }
    }
}
