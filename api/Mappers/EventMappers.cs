using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Event;
using api.Dtos.Organization;
using api.Migrations;
using api.Models;

namespace api.Mappers
{
    public static class EventMappers
    {   
        public static EventDto ToEventDto(this Event eventModel, bool includeOrganization = true, bool includeCertificates = true){
            return new EventDto{
                Id = eventModel.Id,
                EventName = eventModel.EventName,
                Organization = includeOrganization? eventModel.Organization?.ToOrganizationWithoutEventsDto() : null,
                StartDateTime = eventModel.StartDateTime,
                EndDateTime = eventModel.EndDateTime,
                Venue = eventModel.Venue,
                EventDescription = eventModel.EventDescription,
                CertificateFilePath = eventModel.CertificateFilePath,
                QrXPosition = eventModel.QrXPosition,
                QrYPosition = eventModel.QrYPosition,
                TextXPosition = eventModel.TextXPosition,
                TextYPosition = eventModel.TextYPosition,
                Alignment = eventModel.Alignment,
                QrSize = eventModel.QrSize,
                FontSize = eventModel.FontSize,
                IsBold = eventModel.IsBold,
                IsItalic = eventModel.IsItalic,
                IsUnderlined = eventModel.IsUnderlined,
                FontFamily = eventModel.FontFamily,
                Certificates = includeCertificates ? eventModel.Certificates.Select(c => c.ToCertificateDto(withEvent: false)).ToList() : null
            };
        }

        public static Event ToEventFromCreateDto(this CreateEventRequestDto eventDto){
            return new Event{
                EventName = eventDto.EventName,
                OrganizationId = eventDto.OrganizationId,
                StartDateTime = eventDto.StartDateTime,
                EndDateTime = eventDto.EndDateTime,
                Venue = eventDto.Venue,
                EventDescription = eventDto.EventDescription,
                // QrXPosition = eventDto.QrXPosition,
                // QrYPosition = eventDto.QrYPosition
                // OrganizerUserId = eventDto.OrganizerUserId
            };
        }

        public static void UpdateEventFromDto(this Event existingEvent, UpdateEventRequestDto eventDto)
        {
            if (!string.IsNullOrEmpty(eventDto.EventName)) existingEvent.EventName = eventDto.EventName;

            if (eventDto.StartDateTime != default) existingEvent.StartDateTime = eventDto.StartDateTime;

            if (eventDto.EndDateTime != default) existingEvent.EndDateTime = eventDto.EndDateTime;

            if (!string.IsNullOrEmpty(eventDto.Venue))
            {
                existingEvent.Venue = eventDto.Venue;
            }

            if (!string.IsNullOrEmpty(eventDto.CertificateFilePath))
            {
                existingEvent.CertificateFilePath = eventDto.CertificateFilePath;
            }

            if (!string.IsNullOrEmpty(eventDto.EventDescription))
            {
                existingEvent.EventDescription = eventDto.EventDescription;
            }

            existingEvent.QrXPosition = eventDto.QrXPosition;
            existingEvent.QrYPosition = eventDto.QrYPosition;

            existingEvent.TextXPosition = eventDto.TextXPosition;
            existingEvent.TextYPosition = eventDto.TextYPosition;
            existingEvent.FontSize = eventDto.FontSize;

            existingEvent.QrSize = eventDto.QrSize;
            existingEvent.FontFamily = eventDto.FontFamily;

            existingEvent.IsBold = eventDto.IsBold;
            existingEvent.IsItalic = eventDto.IsItalic;
            existingEvent.IsUnderlined = eventDto.IsUnderlined;

            existingEvent.Alignment = eventDto.Alignment;
        }

    }
}