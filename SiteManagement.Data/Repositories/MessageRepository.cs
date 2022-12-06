using SiteManagement.Infrastructure.Context;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;

namespace SiteManagement.Infrastructure.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
