using Microsoft.EntityFrameworkCore;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }
        /// <summary>
        /// Gelen mesajları getirir.opsiyonel olarak belli tarih aralıgında olan measajları getirir
        /// </summary>
        /// <param name="id"></param>
        /// <param name="starDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<ICollection<Message>> GetByReceivedMessage(string id, DateTime? startDate, DateTime? endDate)
        {
            var messagesByReceiver = await _context.Messages
                .Where(x => x.Receiver.Id == id)
                .Where(x=>x.SendDate>=startDate && x.SendDate<=endDate)
                .ToListAsync();
            return messagesByReceiver;
        }
        /// <summary>
        /// Gönderilen mesajları getirir.opsiyonel olarak belli tarih aralıgında olan measajları getirir
        /// </summary>
        /// <param name="id"></param>
        /// <param name="starDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<ICollection<Message>> GetBySendMessage(string id, DateTime? startDate, DateTime? endDate)
        {
            var messagesBySend = await _context.Messages
                .Where(x => x.Sender.Id == id)
                .Where(x => x.SendDate >= startDate && x.SendDate <= endDate)
                .ToListAsync();
            return messagesBySend;
        }

    }
}
