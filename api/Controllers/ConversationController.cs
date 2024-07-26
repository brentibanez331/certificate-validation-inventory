// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using api.Data;
// using api.Dtos.Conversation;
// using api.Interfaces;
// using api.Mappers;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;

// namespace api.Controllers
// {
//     [Route("api/conversation")]
//     [ApiController]
//     public class ConversationController : ControllerBase
//     {
//         private readonly ApplicationDBContext _context;
//         private readonly IConversationRepository _conversationRepo;
//         public ConversationController(ApplicationDBContext context, IConversationRepository conversationRepo)
//         {
//             _conversationRepo = conversationRepo;
//             _context = context;
//         }

//         [HttpGet]
//         public async Task<IActionResult> GetAll(){
//             var conversations = await _conversationRepo.GetAllAsync();
//             var conversationsDto =  conversations.Select(s => s.ToConversationDto());

//             return Ok(conversationsDto);
//         }

//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetById([FromRoute] int id){
//             var conversation = await _conversationRepo.GetByIdAsync(id);

//             if(conversation == null) return NotFound();

//             return Ok(conversation.ToConversationDto());
//         }

//         [HttpPost]
//         public async Task<IActionResult> Create([FromBody] CreateConversationRequestDto conversationDto){
//             var conversationModel = conversationDto.ToConversationFromCreateDto();
//             await _conversationRepo.CreateAsync(conversationModel);

//             return CreatedAtAction(nameof(GetById), new { id = conversationModel.Id }, conversationModel.ToConversationDto());
//         }

//         [HttpPut]
//         [Route("{id}")]
//         public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateConversationRequestDto updateDto){
//             var conversationModel = await _conversationRepo.UpdateAsync(id, updateDto);

//             if(conversationModel == null) return NotFound();

//             return Ok(conversationModel.ToConversationDto());
//         } 


//         [HttpDelete]
//         [Route("{id}")]
//         public async Task<IActionResult> Delete([FromRoute] int id){
//             var conversationModel = await _conversationRepo.DeleteAsync(id);

//             if(conversationModel == null) return NotFound();
            
//             return NoContent();
//         }
//     }
// }