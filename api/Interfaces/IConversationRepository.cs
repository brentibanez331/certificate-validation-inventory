using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Conversation;
using api.Models;

namespace api.Interfaces
{
    public interface IConversationRepository
    {
        Task<List<Conversation>> GetAllAsync();
        Task<Conversation?> GetByIdAsync(int id);
        Task<Conversation> CreateAsync(Conversation conversationModel);
        Task<Conversation?> UpdateAsync(int id, UpdateConversationRequestDto conversationDto);
        Task<Conversation?> DeleteAsync(int id);
    }
}