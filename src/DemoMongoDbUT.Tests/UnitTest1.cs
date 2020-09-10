using DemoMongoDbUT.Infrastructures.Contexts;
using DemoMongoDbUT.Infrastructures.Repositories;
using DemoMongoDbUT.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DemoMongoDbUT.Tests
{
    public class UnitTest1 : MongoIntegrationTest
    {
        private readonly ObjectId _id;
        private readonly Message _expected;
        private readonly IEnumerable<Message> _messages;
        public UnitTest1()
        {
            _id = ObjectId.GenerateNewId();
            _expected = new Message
            {
                Id = _id,
                Body = "Lorem 1",
                CreatedOn = DateTime.UtcNow,
                ReceiverId = 2,
                SenderId = 1,
                UpdatedOn = DateTime.UtcNow
            };
            _messages = new List<Message>
            {
                _expected,
                new Message
                {
                    Id = ObjectId.GenerateNewId(),
                    Body = "Lorem 2",
                    CreatedOn = DateTime.UtcNow,
                    ReceiverId = 2,
                    SenderId = 1,
                    UpdatedOn = DateTime.UtcNow
                },
                new Message
                {
                    Id = ObjectId.GenerateNewId(),
                    Body = "Lorem 3",
                    CreatedOn = DateTime.UtcNow,
                    ReceiverId = 2,
                    SenderId = 1,
                    UpdatedOn = DateTime.UtcNow
                },
                new Message
                {
                    Id = ObjectId.GenerateNewId(),
                    Body = "Lorem 4",
                    CreatedOn = DateTime.UtcNow,
                    ReceiverId = 1,
                    SenderId = 2,
                    UpdatedOn = DateTime.UtcNow
                },
            };
            CreateConnection();
            _collection.InsertMany(_messages);
        }

        [Fact]
        public void Test1()
        {
            var filter = Builders<Message>.Filter.Where(x => x.Id == _id);
            var actual = _collection.Find(filter).FirstOrDefault();

            var filterB = Builders<Message>.Filter.Empty;
            var count = _collection.Find(filterB).CountDocuments();

            Assert.Equal(_expected.Body, actual.Body);
        }

        [Fact]
        public async Task Test2()
        {
            var context = new Mock<IMessageDataContext>();
            context.Setup(x => x.Messages).Returns(_collection);

            ILoggerFactory loggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();
            var repo = new MessageRepository(loggerFactory, context.Object);
            var resultPage1 = await repo.GetByConversationIdAndUserIdAsync(0, 2);
            var resultPage2 = await repo.GetByConversationIdAndUserIdAsync(1, 2);
            var resultPage3 = await repo.GetByConversationIdAndUserIdAsync(2, 2);

            Assert.Equal(2, resultPage1.Count());
            Assert.Equal(2, resultPage2.Count());
            Assert.Empty(resultPage3);
        }
    }
}
