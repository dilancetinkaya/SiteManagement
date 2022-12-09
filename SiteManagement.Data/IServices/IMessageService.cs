using SiteManagement.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices
{
    public interface IMessageService
    {
        Task<MessageDto> GetByIdAsync(int id);

        Task<ICollection<MessageDto>> GetAllAsync();

        Task AddAsync(CreateMessageDto messageDto);
        Task<ICollection<CreateMessageDto>> AddRangeAsync(ICollection<CreateMessageDto> messageDtos);

        Task RemoveAsync(int id);

        UpdateMessageDto Update(UpdateMessageDto messageDto, int id);
        Task<ICollection<MessageDto>> GetMessageByCreateDate();
        Task<ICollection<MessageDto>> GetMessageByReceivedId(string id);
        Task<ICollection<MessageDto>> GetMessageBySendId(string id);
    }
}

