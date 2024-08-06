using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class CreateUserRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public required string Password { get; set; }
        public string Username { get; set; } = string.Empty;
         public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? OrganizationId { get; set; }
    }
}