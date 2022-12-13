﻿using Microsoft.AspNetCore.Mvc;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetMessage()
        {
            var messages = await _messageService.GetAllAsync();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageById(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            return Ok(message);
        }

        [HttpGet("Receiver/{id}")]
        public IActionResult GetMessageByReceivedId(string id, DateTime? starDate, DateTime? endDate)
        {
            var receivedMessages = _messageService.GetMessageByReceivedId(id,starDate,endDate);
            return Ok(receivedMessages);
        }

        [HttpGet("Sender/{id}")]
        public IActionResult GetMessageBySendId(string id, DateTime? starDate, DateTime? endDate)
        {
            var sendMessages = _messageService.GetMessageBySendId(id,starDate,endDate);
            return Ok(sendMessages);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(CreateMessageDto message)
        {
            await _messageService.AddAsync(message);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMessage(UpdateMessageDto message, int id)
        {
            _messageService.Update(message, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _messageService.RemoveAsync(id);
            return Ok();
        }
    }
}
