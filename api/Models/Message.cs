using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public string OrganizationName { get; set; } = string.Empty;

        // public DateTime SentAt { get; set; } = DateTime.Now;
        // public bool IsUser { get; set; }
    }
}