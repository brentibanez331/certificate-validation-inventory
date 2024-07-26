using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public int? EventId { get; set; }
        public Event? Event { get; set; }
        public string CertificateCode { get; set; } = string.Empty;
        public string ParticipantName { get; set; } = string.Empty;
        public string QRCode { get; set; } = string.Empty;
        public DateTime? ExpirationDate { get; set;}
        public bool Revoked { get; set; } = false;
        public DateTime? RevocationDate { get; set; }
        public string? RevocationReason { get; set; } = string.Empty;
    }
}