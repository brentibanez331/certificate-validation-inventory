// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using api.Data;
// using api.Dtos.Message;
// using api.Interfaces;
// using api.Mappers;
// using api.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;

// namespace api.Controllers
// {
//     [Route("api/message")]
//     [ApiController]
//     public class MessageController : ControllerBase
//     {
//         // private readonly ApplicationDBContext _context;
//         private readonly IMessageRepository _messageRepo;
//         public MessageController(IMessageRepository messageRepo)
//         {
//             _messageRepo = messageRepo;
//         }

//         [HttpGet]
//         public async Task<IActionResult> GetAll(){
//             var messages = await _messageRepo.GetAllAsync();
//             var messageDto = messages.Select(s => s.ToMessageDto());
            
//             return Ok(messageDto);
//         }

//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetById([FromRoute] int id){
//             var message = await _messageRepo.GetByIdAsync(id);

//             if(message == null) return NotFound();
//             return Ok(message.ToMessageDto());
//         }

//         [HttpPost]
//         public async Task<IActionResult> Create([FromBody] CreateMessageRequestDto messageDto){
//             var messageModel = messageDto.ToMessageFromCreateDto();
//             await _messageRepo.CreateAsync(messageModel);

//             return CreatedAtAction(nameof(GetById), new {id = messageModel.Id}, messageModel.ToMessageDto());
//         }
//     }
// }