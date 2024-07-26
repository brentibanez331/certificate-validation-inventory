using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Event;
using api.Dtos.User;

namespace api.Dtos.Organization
{
    public class OrganizationDto
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public List<UserDto> Users { get; set; }
        public List<EventDto> Events { get; set; }
        
    }
}