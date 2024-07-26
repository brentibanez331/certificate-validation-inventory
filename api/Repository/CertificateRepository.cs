using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Certificate;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CertificateRepository : ICertificateRepository
    {

        private readonly ApplicationDBContext _context;
        public CertificateRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Certificate>> GetAllAsync()
        {
            return await _context.Certificate.Include(c => c.Event).ThenInclude(e => e.Organization).ToListAsync();
        }

        public async Task<Certificate?> GetByIdAsync(int id)
        {
            return await _context.Certificate.Include(c => c.Event).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Certificate>> GetCertificatesByOrganizerEventCodeAsync(string organizerName, string eventName, string code)
        {
            return await _context.Certificate
                .Include(c => c.Event).ThenInclude(e => e.Organization)
                .Where(e => (string.IsNullOrEmpty(eventName) || e.Event.EventName.Contains(eventName)) &&
                    (string.IsNullOrEmpty(organizerName) || e.Event.Organization.OrganizationName.Contains(organizerName)) &&
                    (string.IsNullOrEmpty(code) || e.CertificateCode.Contains(code)))
                    .ToListAsync();
        }

        public async Task<List<Certificate>> CreateMultipleAsync(CreateCertificateRequestDto certificateDto)
        {
            if (!string.IsNullOrEmpty(certificateDto.EventName))
            {
                var eventEntity = await GetEventByNameAsync(certificateDto.EventName);
                if (eventEntity != null)
                {
                    certificateDto.EventId = eventEntity.Id;
                }
                else
                {
                    throw new ArgumentException("Event not found");
                }
            }

            var certificates = new List<Certificate>();

            foreach (var participantName in certificateDto.ParticipantNames)
            {
                var certificateModel = new Certificate
                {
                    EventId = certificateDto.EventId,
                    ParticipantName = participantName,
                    ExpirationDate = certificateDto.ExpirationDate,
                    CertificateCode = await GenerateUniqueCertificateCodeAsync()
                };

                certificates.Add(certificateModel);
                await _context.Certificate.AddAsync(certificateModel);
            }


            // var certificateModel = certificateDto.ToCertificateFromCreateDto();
            // certificateModel.CertificateCode = await GenerateUniqueCertificateCodeAsync();

            // await _context.Certificate.AddAsync(certificateModel);
            await _context.SaveChangesAsync();

            foreach (var certificate in certificates)
            {
                await _context.Entry(certificate).Reference(c => c.Event).LoadAsync();
                await _context.Entry(certificate.Event).Reference(e => e.Organization).LoadAsync();
            }

            return certificates;
        }
        private async Task<string> GenerateUniqueCertificateCodeAsync()
        {
            string code;
            do
            {
                code = CertificateCodeGenerator.GenerateCode();
            } while (await _context.Certificate.AnyAsync(c => c.CertificateCode == code));

            return code;
        }

        public async Task<Event?> GetEventByNameAsync(string eventName)
        {
            return await _context.Event
                .Where(e => e.EventName.ToLower() == eventName.ToLower())
                .FirstOrDefaultAsync();
        }
    }
}