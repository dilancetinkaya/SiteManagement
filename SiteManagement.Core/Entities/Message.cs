using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManagement.Domain.Entities
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public string MessageContent { get; set; }
        public string SenderId { get; set; }
        public bool IsRead { get; set; } = false;
        public string ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual User Receiver { get; set; }
        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }

    }
}
