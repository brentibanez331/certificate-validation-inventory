using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Certificate;
using api.Models;

namespace api.Interfaces
{
    public interface ICertificateRepository
    {
        Task<List<Certificate>> GetAllAsync();
        Task<Certificate?> GetByIdAsync(int id);
        Task<List<Certificate>> GetCertificatesByOrganizerEventCodeAsync(string organizerName, string eventName, string code, int? eventId);
        Task<List<Certificate>> CreateMultipleAsync(CreateCertificateRequestDto certificateModel);
        Task<byte[]?> GetCertificatePdfByCodeAsync(string code);
        Task<byte[]?> GenerateZipFromCertificateCodes(List<string> certificateCodes);
        // public async Task<IActionResult> DownloadCertificatesAsZip([FromBody] List<string> certificateCodes)

    }
}