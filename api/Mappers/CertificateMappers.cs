using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Certificate;
using api.Models;

namespace api.Mappers
{
    public static class CertificateMappers
    {
        public static CertificateDto ToCertificateDto(this Certificate certificateModel, bool withEvent = true)
        {
            return new CertificateDto
            {
                Id = certificateModel.Id,
                EventId = certificateModel.EventId,
                Event = withEvent ? certificateModel.Event.ToEventDto(includeCertificates: false) : null,
                CertificateCode = certificateModel.CertificateCode,
                ExpirationDate = certificateModel.ExpirationDate,
                Revoked = certificateModel.Revoked,
                QRCode = certificateModel.QRCode,
                ParticipantName = certificateModel.ParticipantName,
                RevocationReason = certificateModel.RevocationReason,
                FilePath = certificateModel.FilePath,
                CreatedAt = certificateModel.CreatedAt
            };
        }

        public static Certificate ToCertificateFromCreateDto(this CreateCertificateRequestDto certificateDto){
            return new Certificate{
                EventId = certificateDto.EventId,
                ExpirationDate = certificateDto.ExpirationDate,
                // ParticipantNames = certificateDto.ParticipantNames
            };
        }
    }
}