using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using SiteManagement.Domain.Entities;
using SiteManagement.Domain.IRepositories;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private const string AllMessageKey = "MESSAGEALL";
        private MemoryCacheEntryOptions _cacheOptions;

        public MessageService(IMessageRepository messageRepository, IMapper mapper, UserManager<User> userManager, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative: TimeSpan.FromMinutes(10));
            _messageRepository = messageRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateMessageDto messageDto)
        {
            if (string.IsNullOrWhiteSpace(messageDto.ReceiverId))
            {
                var users = await _userManager.GetUsersInRoleAsync("Admin");
                if (users is not null && users.Any())
                {
                    messageDto.ReceiverId = users.First()?.Id;
                }
            }
            var message = _mapper.Map<Message>(messageDto);
            await _messageRepository.AddAsync(message);
            _memoryCache.Remove(AllMessageKey);
        }

        public async Task<ICollection<MessageDto>> GetAllAsync()
        {
            var messages = await _messageRepository.GetAllAsync();

            return _mapper.Map<ICollection<MessageDto>>(messages);
        }

        public async Task<MessageDto> GetByIdAsync(int id)
        {
            return await _memoryCache.GetOrCreateAsync(AllMessageKey, async flatsCache =>
            {
                flatsCache.SetOptions(_cacheOptions);
                var message = await _messageRepository.GetByIdAsync(id);
                if (message is null) throw new Exception("Message is not found");

                return _mapper.Map<MessageDto>(message);
            });
        }

        /// <summary>
        /// Kullanıcının Gelen mesajlarını görüntüleme
        /// </summary>
        public async Task<ICollection<MessageDto>> GetMessageByReceivedId(string id, DateTime? startDate, DateTime? endDate)
        {
            var receivedMessage = await _messageRepository.GetByReceivedMessage(id, startDate, endDate);
            var receivedMessageDto = _mapper.Map<ICollection<MessageDto>>(receivedMessage);
            return receivedMessageDto;
        }

        /// <summary>
        /// Kullanıcının gönderdiği mesajlarını görüntüleme
        /// </summary>
        public async Task<ICollection<MessageDto>> GetMessageBySendId(string id, DateTime? startDate, DateTime? endDate)
        {
            var sentMessage = await _messageRepository.GetBySendMessage(id, startDate, endDate);
            var sentMessageDto = _mapper.Map<ICollection<MessageDto>>(sentMessage);
            return sentMessageDto;
        }

        public async Task RemoveAsync(int id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            if (message is null) throw new Exception("Message is not found");

            _messageRepository.Remove(message);
            _memoryCache.Remove(AllMessageKey);
        }

        public async Task UpdateAsync(UpdateMessageDto messageDto, int id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            if (message is null) throw new Exception("Message is not found");

            message.MessageContent = messageDto.MessageContent;
            message.IsRead = messageDto.IsRead;
            _messageRepository.Update(message);
            _memoryCache.Remove(AllMessageKey);
        }
    }
}
