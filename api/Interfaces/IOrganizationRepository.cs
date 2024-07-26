using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Organization;
using api.Models;

namespace api.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<List<Organization>> GetAllAsync();
        Task<Organization?> GetByIdAsync(int id);
        Task<Organization> CreateAsync(Organization organizationModel);
        Task<Organization?> UpdateAsync(int id, UpdateOrganizationRequestDto organizationDto);
        Task<Organization?> DeleteAsync(int id);
    }
}