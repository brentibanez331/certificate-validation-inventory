using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string EventName { get; set; } = string.Empty;
        public int? OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        // public int? OrganizerUserId { get; set; }
        // public User? OrganizerUser { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public string CertificateFilePath { get; set; } = string.Empty;
        public List<Certificate> Certificates { get; set; } = [];
    }
}