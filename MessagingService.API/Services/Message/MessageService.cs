using MessagingService.API.Data.Entities;
using MessagingService.API.Data.Repositories;
using MessagingService.API.Models.Response;
using Microsoft.EntityFrameworkCore.Internal;
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

        public async Task<BaseResponse<Messages>> SendMessage(Messages model)
        {
            var response = new BaseResponse<Messages>();

            var user = await _userRepository.GetByUserNameAsync(model.FromUserName);
            var toUser = await _userRepository.GetByUserNameAsync(model.ToUserName);

            model.FromId = user.Id;
            model.SendToId = toUser.Id;

            var blocked = new BlockList
            {
                UserId = user.Id,
                BlockedUserId = toUser.Id
            };

            if (toUser != null)
            {
                var isBlocked = await _blockRepository.IsBlocked(blocked);
                if (isBlocked == null)
                {
                    var result = await _messageRepository.InsertAsync(model);
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

        public async Task<BaseResponse<List<Messages>>> GetMyMessagesAsync(string userName)
        {
            var response = new BaseResponse<List<Messages>>();

            var user = await _userRepository.GetByUserNameAsync(userName);

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
