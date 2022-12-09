﻿using SiteManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Domain.IRepositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<ICollection<Message>> GetByReceivedMessage(string id);
        Task<ICollection<Message>> GetBySendMessage(string id,DateTime? starDate, DateTime? endDate);//tarih filteresi aralık verebilir nullable datetime

    }
}
