using SiteManagement.Infrastructure.Context;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SiteManagement.Infrastructure.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ICollection<Message>> GetByReceivedMessage(string id)
        {
            var messagesByReceiver =  _context.Messages.Where(x => x.Receiver.Id == id).ToList();
            return  messagesByReceiver;
        }

        public async Task<ICollection<Message>> GetBySendMessage(string id)
        {
            var messagesBySend = _context.Messages.Where(x => x.Sender.Id == id).ToList();
            return messagesBySend;
        }

        public Task<ICollection<Message>> GetMessageByCreateDate()
        {
            throw new System.NotImplementedException();
        }
    }
}
