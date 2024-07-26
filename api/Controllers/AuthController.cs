using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Conversation;
using api.Dtos.User;
using api.Exceptions;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public AuthController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            try
            {
                var userModel = userDto.ToUserFromCreateDto();
                var createdUser = await _userRepo.CreateAsync(userModel);

                return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, createdUser.ToUserDto());
            }
            catch (EmailAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] CreateUserRequestDto userDto)
        {
            var userModel = await _userRepo.GetByEmailAndPasswordAsync(userDto.Email, userDto.Password);

            if (userModel == null) { return Unauthorized(new { message = "Invalid email or password" }); }

            return Ok(userModel.ToUserDto());
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllAsync();
            var usersDto = users.Select(s => s.ToUserDto());

            return Ok(usersDto);
        }

        [HttpGet]
        [Route("users/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _userRepo.GetByIdAsync(id);

            if (user == null) return NotFound();

            return Ok(user.ToUserDto());
        }
    }
}