using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Event;
using api.Dtos.Organization;

namespace api.Dtos.Certificate
{
    public class CertificateDto
    {
        public int Id { get; set; }
        public string CertificateCode { get; set; } = string.Empty;
        public string QRCode { get; set; } = string.Empty;
        public DateTime? ExpirationDate { get; set; }
        public bool Revoked { get; set; } = false;
        public string ParticipantName = string.Empty;
        public DateTime RevocationDate { get; set; }
        public string RevocationReason { get; set; } = string.Empty;
        public int? EventId { get; set; }
        public EventDto? Event { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }
}