using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Certificate;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Utilities;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
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

        public async Task<List<Certificate>> GetCertificatesByOrganizerEventCodeAsync(string organizerName, string eventName, string code, int? eventId)
        {
            return await _context.Certificate
                .Include(c => c.Event).ThenInclude(e => e.Organization)
                .Where(e => (eventId == null || e.EventId == eventId) && (string.IsNullOrEmpty(eventName) || e.Event.EventName.Contains(eventName)) &&
                    (string.IsNullOrEmpty(organizerName) || e.Event.Organization.OrganizationName.Contains(organizerName)) &&
                    (string.IsNullOrEmpty(code) || e.CertificateCode.Contains(code)))
                    .ToListAsync();
        }

        public async Task<List<Certificate>> CreateMultipleAsync(CreateCertificateRequestDto certificateDto)
        {

            var eventEntity = await _context.Event.Where(e => e.Id == certificateDto.EventId).Include(e => e.Certificates).FirstOrDefaultAsync()
                                ?? throw new ArgumentException("Event not found");

            var certificates = new List<Certificate>();

            var relativePath = eventEntity.CertificateFilePath.Replace("/static", "").TrimStart('/');
            var baseCertificatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.Replace('/', Path.DirectorySeparatorChar));

            foreach (var participantName in certificateDto.ParticipantNames)
            {
                var uniqueCode = await GenerateUniqueCertificateCodeAsync();
                var certificateSavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "certificates", $"{uniqueCode}.png");

                var certificateModel = new Certificate
                {
                    EventId = certificateDto.EventId,
                    ParticipantName = participantName,
                    ExpirationDate = certificateDto.ExpirationDate,
                    CertificateCode = uniqueCode,
                    FilePath = $"/static/certificates/{uniqueCode}.png"
                };

                certificates.Add(certificateModel);
                await _context.Certificate.AddAsync(certificateModel);

                var certificateGenerator = new CertificateGenerator();
                var certificateWithQR = certificateGenerator.GenerateCertificateWithQR(
                    participantName,
                    eventEntity.TextXPosition,
                    eventEntity.TextYPosition,
                    eventEntity.Alignment,
                    uniqueCode,
                    baseCertificatePath,
                    (int)eventEntity.QrXPosition,  // Ensure correct type casting if necessary
                    (int)eventEntity.QrYPosition,
                    eventEntity.IsBold,
                    eventEntity.IsItalic,
                    eventEntity.IsUnderlined,
                    fontFamily: eventEntity.FontFamily,
                    fontSize: eventEntity.FontSize,
                    qrCodeSize: eventEntity.QrSize
                );


                await File.WriteAllBytesAsync(certificateSavePath, certificateWithQR);
            }

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

        public async Task<byte[]?> GenerateZipFromCertificateCodes(List<string> certificateCodes){
            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var code in certificateCodes)
                {
                    var pdfBytes = await GetCertificatePdfByCodeAsync(code);
                    if (pdfBytes != null && pdfBytes.Length > 0)
                    {
                        var zipEntry = archive.CreateEntry($"{code}.pdf", CompressionLevel.Optimal);
                        using var zipStream = zipEntry.Open();
                        await zipStream.WriteAsync(pdfBytes);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to generate PDF for certificate code: {code}");
                    }
                }
            }
            return memoryStream.ToArray();
        }

        public async Task<byte[]?> GetCertificatePdfByCodeAsync(string code)
        {
            var certificate = await _context.Certificate.FirstOrDefaultAsync(c => c.CertificateCode == code);

            if (certificate == null)
            {
                Console.WriteLine("Certificate Is Null");
                return null;
            }

            string relativePath = certificate.FilePath.Replace("/static", "").TrimStart('/');
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.Replace('/', Path.DirectorySeparatorChar));
            Console.WriteLine($"Full Path: {fullPath}");

            if (File.Exists(fullPath))
            {
                using (var ms = new MemoryStream())
                {
                    var imageData = await File.ReadAllBytesAsync(fullPath);
                    var image = ImageDataFactory.Create(imageData);

                    float imageWidth = image.GetWidth();
                    float imageHeight = image.GetHeight();

                    var pdfWriter = new PdfWriter(ms);
                    var pdfDocument = new PdfDocument(pdfWriter);
                    var pageSize = new iText.Kernel.Geom.PageSize(imageWidth, imageHeight);
                    pdfDocument.SetDefaultPageSize(pageSize);

                    // Load and add the image
                    var document = new Document(pdfDocument);

                    // Add the image to the document
                    var pdfImg = new iText.Layout.Element.Image(image);
                    pdfImg.SetFixedPosition(0, 0);
                    pdfImg.SetWidth(imageWidth);
                    pdfImg.SetHeight(imageHeight);

                    document.Add(pdfImg);
                    document.Close();

                    return ms.ToArray();
                }
            }
            else
            {
                Console.WriteLine($"File not found at: {fullPath}");
            }
            return null;
        }
    }
}