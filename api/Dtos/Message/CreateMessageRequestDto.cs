using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Message
{
    public class CreateMessageRequestDto
    {

        public int? ConversationId { get; set; }
        public string Text { get; set; } = string.Empty;
        // public DateTime SentAt { get; set; } = DateTime.Now;
        public bool IsUser { get; set; } 

    }
}