using MessagingService.API.Data.Entities;
using MessagingService.API.Data.Repositories;
using MessagingService.API.Models.Request;
using MessagingService.API.Models.Response;
using Microsoft.EntityFrameworkCore.Internal;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingService.API.Services.Message
{
    public class MessageService : IMessageService
    {
        private readonly MessageRepository _messageRepository;
        private readonly UserRepository _userRepository;
        private readonly BlockRepository _blockRepository;

        public MessageService(MessageRepository messageRepository, UserRepository userRepository, BlockRepository blockRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _blockRepository = blockRepository;
        }

        public async Task<BaseResponse<Messages>> SendMessage(RequestMessageModel request)
        {
            var response = new BaseResponse<Messages>();
            var toUser = await _userRepository.GetByUserNameAsync(request.ToUserName);

            if (toUser != null)
            {
                var entity = new Messages
                {
                    FromId = new ObjectId(request.FromId),
                    SendToId = toUser.Id,
                    FromUserName = request.FromUserName,
                    ToUserName = request.ToUserName,
                    Message = request.Message
                };
                var isBlocked = await _blockRepository.IsBlocked(new BlockList
                {
                    UserId = entity.FromId,
                    BlockedUserId = toUser.Id
                });

                if (isBlocked == null)
                {
                    var result = await _messageRepository.InsertAsync(entity);
                    if (result.Id != default)
                    {
                        response.Data = result;
                        return response;
                    }
                }
                else
                {
                    response.Errors.Add("Blocklandığınız için mesaj gönderemezsiniz!");
                    return response;
                }
            }
            response.Errors.Add("Kullanıcı bulunamadı!");
            return response;
        }

        public async Task<BaseResponse<List<Messages>>> GetMyMessagesAsync(string username)
        {
            var response = new BaseResponse<List<Messages>>();

            var user = await _userRepository.GetByUserNameAsync(username);

            var messages = await _messageRepository.GetMyMessagesAsync(user.Id);
            if (messages != null)
            {
                response.Data = messages;
                return response;
            }
            response.Errors.Add("Mesaj bulunamadı!");

            return response;
        }
    }
}
