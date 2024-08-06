using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Event;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Utilities;
using Aspose.Pdf.Devices;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepo;

        public EventController(IEventRepository eventRepo)
        {
            _eventRepo = eventRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? organization, [FromQuery] string? eventName, [FromQuery] int? organizationId)
        {
            List<Event> events;
            if (!string.IsNullOrEmpty(organization) || !string.IsNullOrEmpty(eventName) || organizationId != null)
            {
                events = await _eventRepo.GetEventsByOrganizerAndEventNameAsync(organization, eventName, (int)organizationId);
            }
            else
            {
                events = await _eventRepo.GetAllAsync();
            }
            var eventsDto = events.Select(e => e.ToEventDto());

            return Ok(eventsDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var _event = await _eventRepo.GetByIdAsync(id);

            if (_event == null) return NotFound();

            return Ok(_event.ToEventDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateEventRequestDto eventDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Any())
                    .Select(ms =>
                    {
                        var displayName = eventDto.GetType()
                            .GetProperty(ms.Key)?
                            .GetCustomAttributes(typeof(DisplayNameAttribute), false)
                            .Cast<DisplayNameAttribute>()
                            .FirstOrDefault()?.DisplayName ?? ms.Key;
                        var errorMessages = ms.Value.Errors.Select(e => e.ErrorMessage).ToList();
                        return new { Field = displayName, Errors = errorMessages };
                    })
                    .ToList();

                return BadRequest(new { errors });
            }

            var eventModel = eventDto.ToEventFromCreateDto();

            if (eventDto.CertificateFile != null)
            {
                var fileExtension = Path.GetExtension(eventDto.CertificateFile.FileName);
                var uniqueFilename = CertificateFilePathNameGenerator.GenerateUniqueFilename(fileExtension);
                var filePath = Path.Combine("wwwroot", "uploads", uniqueFilename);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await eventDto.CertificateFile.CopyToAsync(stream);
                }

                eventModel.CertificateFilePath = $"/static/uploads/{uniqueFilename}";
            }

            await _eventRepo.CreateAsync(eventModel);

            return CreatedAtAction(nameof(GetById), new { id = eventModel.Id }, eventModel.ToEventDto());
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEventRequestDto eventDto)
        {
            var eventModel = await _eventRepo.UpdateAsync(id, eventDto);

            if (eventModel == null) return NotFound();

            return Ok(eventModel.ToEventDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var eventModel = await _eventRepo.DeleteAsync(id);

            if (eventModel == null) return NotFound();

            return NoContent();
        }
    }
}