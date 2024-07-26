using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Event
{
    public class CreateEventRequestDto
    {
        public string EventName { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
        // public int OrganizerUserId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Venue { get; set; } = string.Empty;
        // public string CertificateFilePath { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public IFormFile? CertificateFile { get; set; }
    }
}