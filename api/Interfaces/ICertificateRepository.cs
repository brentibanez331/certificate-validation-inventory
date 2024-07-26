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
        Task<List<Certificate>> GetCertificatesByOrganizerEventCodeAsync(string organizerName, string eventName, string code);
        Task<List<Certificate>> CreateMultipleAsync(CreateCertificateRequestDto certificateModel);
    }
}