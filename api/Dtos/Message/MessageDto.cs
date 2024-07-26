using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        // public Conversation? Conversation { get; set; }
        public int? ConversationId { get; set; }

        public string Text { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.Now;
        public bool IsUser { get; set; }
    }
}