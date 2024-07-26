using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Conversation;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDBContext _context;
        public ConversationRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Conversation> CreateAsync(Conversation conversationModel)
        {
            await _context.Conversation.AddAsync(conversationModel);
            await _context.SaveChangesAsync();
            return conversationModel;
        }

        public async Task<List<Conversation>> GetAllAsync()
        {
            return await _context.Conversation.Include(c => c.Messages).ToListAsync();
        }

        public async Task<Conversation?> GetByIdAsync(int id)
        {
            return await _context.Conversation.Include(c => c.Messages).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Conversation?> DeleteAsync(int id)
        {
            var conversationModel = await _context.Conversation.Include(c => c.Messages).FirstOrDefaultAsync(x => x.Id == id);

            if (conversationModel == null) return null;

            _context.Conversation.Remove(conversationModel);
            await _context.SaveChangesAsync();
            return conversationModel;
        }

        

        public async Task<Conversation?> UpdateAsync(int id, UpdateConversationRequestDto conversationDto)
        {
            var existingConversation = await _context.Conversation.FirstOrDefaultAsync(x => x.Id == id);

            if(existingConversation == null) return null;

            existingConversation.Title = conversationDto.Title;
            existingConversation.LastSent = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingConversation;
        }
    }
}