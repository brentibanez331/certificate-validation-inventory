using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace api.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime LastSent { get; set; } = DateTime.Now;

        public List<Message> Messages { get; set; } = [];

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}