using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Organization;
using api.Dtos.User;
using api.Models;

namespace api.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                Id = userModel.Id,
                Email = userModel.Email,
                Password = userModel.Password,
                Username = userModel.Username,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                OrganizationId = userModel.OrganizationId
                // Organization = userModel.Organization != null ? new OrganizationDto
                // {
                //     Id = userModel.Organization.Id,
                //     OrganizationName = userModel.Organization.OrganizationName
                // } : null
            };
        }

        public static User ToUserFromCreateDto(this CreateUserRequestDto userDto)
        {
            return new User
            {
                Email = userDto.Email,
                Password = userDto.Password,
                Username = userDto.Username,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                OrganizationId = userDto.OrganizationId
            };
        }
    }
}