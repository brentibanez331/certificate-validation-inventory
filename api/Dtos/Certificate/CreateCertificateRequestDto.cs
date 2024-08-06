using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Certificate
{
    public class CreateCertificateRequestDto
    {
        public int? EventId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        // public string EventName { get; set; } = string.Empty;
        // public string ParticipantName { get; set; } = string.Empty;
        public List<string> ParticipantNames { get; set; } = [];
    }
}