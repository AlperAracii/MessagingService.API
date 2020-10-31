using MessagingService.API.Data.Entities;
using MessagingService.API.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingService.API.Services.Message
{
    public interface IMessageService
    {
        Task<BaseResponse<Messages>> SendMessage(Messages model);
        Task<BaseResponse<List<Messages>>> GetMyMessagesAsync(string userName);
    }
}
