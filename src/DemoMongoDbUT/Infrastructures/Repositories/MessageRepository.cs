using DemoMongoDbUT.Infrastructures.Contexts;
using DemoMongoDbUT.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoMongoDbUT.Infrastructures.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ILogger<MessageRepository> _logger;
        private readonly IMongoCollection<Message> _messages;
        public MessageRepository(ILoggerFactory loggerFactory, IMessageDataContext _context)
        {
            _logger = loggerFactory.CreateLogger<MessageRepository>();
            if (_context == null)
                throw new ArgumentNullException(nameof(MessageRepository));
            _messages = _context.Messages;
        }

        public async Task<IEnumerable<Message>> GetByConversationIdAndUserIdAsync(int pageIndex, int pageSize)
        {
            var filter = Builders<Message>.Filter.Empty;
            var messages = await _messages.Find(filter)
                                    .Skip(pageIndex * pageSize)
                                    .Limit(pageSize)
                                    .ToListAsync();
            return messages;
        }
    }
}
