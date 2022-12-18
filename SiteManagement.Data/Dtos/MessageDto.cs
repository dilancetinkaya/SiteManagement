using System;

namespace SiteManagement.Infrastructure.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string MessageContent { get; set; }
        public DateTime SendDate { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
