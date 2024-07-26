using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Organization
{
    public class CreateOrganizationRequestDto
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
    }
}