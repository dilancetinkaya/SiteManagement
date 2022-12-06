using AutoMapper;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Service.Services
{
    public class MessageService : IMessageService
    {
        public Task AddAsync(CreateMessageDto messageDto)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<MessageDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MessageDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public UpdateMessageDto Update(UpdateMessageDto messageDto, int id)
        {
            throw new NotImplementedException();
        }
    }
}
