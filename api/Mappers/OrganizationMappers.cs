using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Event;
using api.Dtos.Organization;
using api.Models;

namespace api.Mappers
{
    public static class OrganizationMappers
    {
        public static OrganizationDto ToOrganizationDto(this Organization organizationModel){
            return new OrganizationDto{
                Id = organizationModel.Id,
                OrganizationName = organizationModel.OrganizationName,
                Users = organizationModel.Users.Select(c => c.ToUserDto()).ToList(),
                Events = organizationModel.Events.Select(e => e.ToEventDto(false)).ToList(),
            };
        }

        public static Organization ToOrganizationFromCreateDto(this CreateOrganizationRequestDto organizationDto){
            return new Organization{
                OrganizationName = organizationDto.OrganizationName
            };
        }

        public static OrganizationWithoutEventsDto ToOrganizationWithoutEventsDto(this Organization organizationModel){
            return new OrganizationWithoutEventsDto{
                Id = organizationModel.Id,
                OrganizationName = organizationModel.OrganizationName,
                Users = organizationModel.Users.Select(c => c.ToUserDto()).ToList()
            };
        }
    }
}