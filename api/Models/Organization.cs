using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public List<User> Users { get; set; } = [];
        public List<Event> Events { get; set; } = [];
    }
}