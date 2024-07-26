using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Event
{
    public class UpdateEventRequestDto
    {
        public string EventName { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string CertificateFilePath { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
    }
}