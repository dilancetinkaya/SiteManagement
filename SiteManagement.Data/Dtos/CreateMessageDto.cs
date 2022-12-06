using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.Dtos
{
    public class CreateMessageDto
    {
        public string MessageContent { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public bool IsRead { get; set; }
    }
}
