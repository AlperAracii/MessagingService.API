using MessagingService.API.Data.Entities;
using MessagingService.API.Models.Request;
using MessagingService.API.Models.Response;
using MongoDB.Driver.Core.WireProtocol.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingService.API.Services.Message
{
    public interface IMessageService
    {
        Task<BaseResponse<Messages>> SendMessage(RequestMessageModel request);
        Task<BaseResponse<List<Messages>>> GetMyMessagesAsync(string username);
    }
}
