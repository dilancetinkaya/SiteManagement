using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Infrastructure.Dtos;
using SiteManagement.Infrastructure.IServices;
using System;
using System.Threading.Tasks;

namespace SiteManagement.API.Controllers
{
    [Authorize(Roles = "Admin,User", AuthenticationSchemes = "Bearer")]
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
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

        [HttpGet("receiver/{id}")]
        public async Task<IActionResult> GetMessageByReceivedIdAsync(string id, DateTime? startDate, DateTime? endDate)
        {
            var receivedMessages = await _messageService.GetMessageByReceivedId(id, startDate, endDate);
            return Ok(receivedMessages);
        }

        [HttpGet("sender/{id}")]
        public async Task<IActionResult> GetMessageBySendIdAsync(string id, DateTime? startDate, DateTime? endDate)
        {
            var sendMessages = await _messageService.GetMessageBySendId(id, startDate, endDate);
            return Ok(sendMessages);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(CreateMessageDto message)
        {
            await _messageService.AddAsync(message);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessageAsync(UpdateMessageDto message, int id)
        {
            await _messageService.UpdateAsync(message, id);
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
