using SiteManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Domain.IRepositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<ICollection<Message>> GetByReceivedMessage(string id);
        Task<ICollection<Message>> GetBySendMessage(string id);
        Task<ICollection<Message>> GetMessageByCreateDate();

    }
}
