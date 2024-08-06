using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Event
{
    public class CreateEventRequestDto
    {
        [DisplayName("Event Name")]
        public required string EventName { get; set; }
        public int OrganizationId { get; set; }
        // public int OrganizerUserId { get; set; }

        [DisplayName("Event Date")]
        public required DateTime StartDateTime { get; set; }
        public required DateTime EndDateTime { get; set; }

        [DisplayName("Event Venue")]
        public required string Venue { get; set; }
        // public string CertificateFilePath { get; set; } = string.Empty;
        [DisplayName("Event Description")]
        public string EventDescription { get; set; } = string.Empty;
        // public double QrXPosition { get; set; }
        // public double QrYPosition { get; set; }

        [DisplayName("Certificate File")]
        public IFormFile? CertificateFile { get; set; }
    }
}