using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Message;
using api.Models;

namespace api.Dtos.Conversation
{
    public class ConversationDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime LastSent { get; set; }

        public List<MessageDto> Messages { get; set; }

        // public List<Message> Messages { get; set; } = [];
        // Messages
    }
}