using MessagingService.API.Data.Entities;
using MessagingService.API.Models.Request;
using MessagingService.API.Models.Response;
using System.Threading.Tasks;

namespace MessagingService.API.Services.Account
{
    public interface IAccountService
    {
        Task<BaseResponse<BlockList>> BlockUserAsync(RequestBlockModel model);
        Task<BaseResponse<BlockList>> UnblockUserAsync(RequestBlockModel request);
    }
}
