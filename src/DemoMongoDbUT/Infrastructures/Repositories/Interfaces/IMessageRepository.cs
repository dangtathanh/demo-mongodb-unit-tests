using DemoMongoDbUT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoMongoDbUT.Infrastructures.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetByConversationIdAndUserIdAsync(int pageIndex, int pageSize);
    }
}
