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
                // OrganizerUser = eventModel.OrganizerUser?.ToUserDto(),
                StartDateTime = eventModel.StartDateTime,
                EndDateTime = eventModel.EndDateTime,
                Venue = eventModel.Venue,
                EventDescription = eventModel.EventDescription,
                CertificateFilePath = eventModel.CertificateFilePath,
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
                // OrganizerUserId = eventDto.OrganizerUserId
            };
        }

    }
}