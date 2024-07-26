using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;

namespace api.Dtos.Organization
{
    public class OrganizationWithoutEventsDto
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public List<UserDto> Users { get; set; } = [];
    }
}