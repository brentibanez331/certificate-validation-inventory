using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Exceptions;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User userModel)
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == userModel.Email);

            if (existingUser != null)
            {
                throw new EmailAlreadyExistsException("User with this email already exists");
            }

            await _context.User.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return await _context.User.Include(c => c.Organization).FirstAsync(u => u.Id == userModel.Id);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.User.Include(c => c.Organization).FirstAsync(c => c.Id == id);
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.User.Include(c => c.Organization).FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.User.Include(c => c.Organization).ToListAsync();
        }
    }
}