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
        public double QrXPosition { get; set; }
        public double QrYPosition { get; set; }
        public double QrSize { get; set; }
        public double TextXPosition { get; set; }
        public double TextYPosition { get; set; }
        public string Alignment { get; set; } = "left";
        public int FontSize { get; set; }
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public bool IsUnderlined { get; set; }
        public string FontFamily {get;set;}
    }
}