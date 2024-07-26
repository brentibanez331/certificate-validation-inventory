using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Event;
using api.Dtos.Organization;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDBContext _context;

        public EventRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Event.Include(e => e.Organization).Include(e => e.Certificates).ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Event.Include(e => e.Organization).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Event>> GetEventsByOrganizerAndEventNameAsync(string organizerName, string eventName)
        {
            return await _context.Event
                .Include(e => e.Organization).Where(e => (string.IsNullOrEmpty(organizerName) || e.Organization.OrganizationName.Contains(organizerName)) &&
                (string.IsNullOrEmpty(eventName) || e.EventName.Contains(eventName)))
                .ToListAsync();
        }

        public async Task<Event> CreateAsync(Event eventModel)
        {
            await _context.Event.AddAsync(eventModel);
            await _context.SaveChangesAsync();
            return await _context.Event.Include(e => e.Organization).ThenInclude(o => o.Users).FirstAsync(e => e.Id == eventModel.Id);
        }

        public async Task<Event?> UpdateAsync(int id, UpdateEventRequestDto eventDto){
            var existingEvent = await _context.Event.FirstOrDefaultAsync(x => x.Id == id);

            if(existingEvent == null) return null;

            existingEvent.EventName = eventDto.EventName;

            await _context.SaveChangesAsync();
            return existingEvent;
        }

        public async Task<Event?> DeleteAsync(int id){
            var eventModel = await _context.Event.FirstOrDefaultAsync(x => x.Id == id);

            if (eventModel == null) return null;

            _context.Event.Remove(eventModel);
            await _context.SaveChangesAsync();
            return eventModel;
        }
    }
}