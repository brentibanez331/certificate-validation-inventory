// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using api.Dtos.Conversation;
// using api.Models;

// namespace api.Mappers
// {
//     public static class ConversationMappers
//     {
//         public static ConversationDto ToConversationDto(this Conversation conversationModel){
//             return new ConversationDto{
//                 Id = conversationModel.Id,
//                 Title = conversationModel.Title,
//                 LastSent = conversationModel.LastSent,
//                 Messages = conversationModel.Messages.Select(c => c.ToMessageDto()).ToList()
//             };
//         }

//         public static Conversation ToConversationFromCreateDto(this CreateConversationRequestDto conversationDto){
//             return new Conversation{
//                 Title = conversationDto.Title
//             };
//         }
//     }
// }