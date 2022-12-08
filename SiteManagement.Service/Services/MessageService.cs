using AutoMapper;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateMessageDto messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);
            await _messageRepository.AddAsync(message);
        }

        public async Task<ICollection<MessageDto>> GetAllAsync()
        {
            var messages = await _messageRepository.GetAllAsync();
            return _mapper.Map<ICollection<MessageDto>>(messages);
        }

        public async Task<MessageDto> GetByIdAsync(int id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            if (message is null) throw new Exception("Message is not found");

            return _mapper.Map<MessageDto>(message);
        }

        public Task<ICollection<MessageDto>> GetMessageByCreateDate()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<MessageDto>> GetMessageByReceivedId(string id)
        {
            var receivedMessage =await _messageRepository.GetByReceivedMessage(id);
            var receivedMessageDto = _mapper.Map<ICollection<MessageDto>>(receivedMessage);
            return receivedMessageDto;
        }

        public async Task<ICollection<MessageDto>> GetMessageBySendId(string id)
        {
            var sentMessage = await _messageRepository.GetBySendMessage(id);
            var sentMessageDto = _mapper.Map<ICollection<MessageDto>>(sentMessage);
            return sentMessageDto;
        }

        public async Task RemoveAsync(int id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            if ( message is null) throw new Exception("Message is not found");

            _messageRepository.Remove(message);
        }

        public UpdateMessageDto Update(UpdateMessageDto messageDto, int id)
        {
            var updatedMessage = _mapper.Map<Message>(messageDto);
            updatedMessage.Id = id;
            _messageRepository.Update(updatedMessage);
            return messageDto;
        }
    }
}
