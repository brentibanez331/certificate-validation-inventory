using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Conversation
{
    public class CreateConversationRequestDto
    {
        public string Title { get; set; } = string.Empty;
        // public DateTime LastSent { get; set; }
    }
}