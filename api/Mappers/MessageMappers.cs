// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using api.Dtos.Message;
// using api.Models;

// namespace api.Mappers
// {
//     public static class MessageMappers
//     {
//         public static MessageDto ToMessageDto(this Message messageModel){
//             return new MessageDto{
//                 Id = messageModel.Id,
//                 ConversationId = messageModel.ConversationId,
//                 Text = messageModel.Text,
//                 SentAt = messageModel.SentAt,
//                 IsUser = messageModel.IsUser
//             };
//         }

//         public static Message ToMessageFromCreateDto(this CreateMessageRequestDto messageDto){
//             return new Message{
//                 ConversationId = messageDto.ConversationId,
//                 Text = messageDto.Text,
//                 IsUser = messageDto.IsUser
//             };
//         }
//     }
// }