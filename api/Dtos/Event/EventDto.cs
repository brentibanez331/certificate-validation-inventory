using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Certificate;
using api.Dtos.Organization;
using api.Dtos.User;

namespace api.Dtos.Event
{
    public class EventDto
    {
        public int Id { get; set; }
        public string EventName { get; set; } = string.Empty;
        public OrganizationWithoutEventsDto? Organization { get; set; }
        // public int? OrganizerUserId { get; set; }
        // public UserDto? OrganizerUser { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public string CertificateFilePath { get; set; } = string.Empty;
        public double QrXPosition { get; set; }
        public double QrYPosition { get; set; }
        public double QrSize { get; set; }
        public double TextXPosition { get; set; }
        public double TextYPosition { get; set; }
        public string Alignment { get; set; } = "left";
        public int FontSize { get; set; }
        public List<CertificateDto> Certificates { get; set; }
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public bool IsUnderlined { get; set; }
        public string FontFamily {get;set;}
    }
}