using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Certificate
{
    public class CertificateWithoutEventsDto
    {
        public int Id { get; set; }
        public string CertificateCode { get; set; } = string.Empty;
        public string QRCode { get; set; } = string.Empty;
        public DateTime? ExpirationDate { get; set; }
        public bool Revoked { get; set; } = false;
        public DateTime RevocationDate { get; set; }
        public string RevocationReason { get; set; } = string.Empty;
    }
}