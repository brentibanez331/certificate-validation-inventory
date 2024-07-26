using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Organization;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDBContext _context;

        public OrganizationRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Organization> CreateAsync(Organization organizationModel)
        {
            await _context.Organization.AddAsync(organizationModel);
            await _context.SaveChangesAsync();
            return organizationModel;
        }

        public async Task<Organization?> DeleteAsync(int id)
        {
            var organizationModel = await _context.Organization.Include(c => c.Users).FirstOrDefaultAsync(x => x.Id == id);

            if (organizationModel == null) return null;

            _context.Organization.Remove(organizationModel);
            await _context.SaveChangesAsync();
            return organizationModel;
        }

        public async Task<List<Organization>> GetAllAsync()
        {
            return await _context.Organization
                .Include(o => o.Users)
                .Include(o => o.Events)
                .ToListAsync();
        }

        public async Task<Organization?> GetByIdAsync(int id)
        {
            return await _context.Organization.Include(c => c.Users).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Organization?> UpdateAsync(int id, UpdateOrganizationRequestDto organizationDto)
        {
            var existingOrganization = await _context.Organization.FirstOrDefaultAsync(x => x.Id == id);

            if (existingOrganization == null) return null;

            existingOrganization.OrganizationName = organizationDto.OrganizationName;

            await _context.SaveChangesAsync();
            return existingOrganization;
        }
    }
}