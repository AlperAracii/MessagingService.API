using MessagingService.API.Data.Entities;
using MessagingService.API.Models.Request;
using MessagingService.API.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingService.API.Services.Users
{
    public interface IUserService
    {
        Task<BaseResponse<User>> GetUserByIdAsync(string id);

        Task<List<User>> GetAllUsersAsync();

        Task<BaseResponse<User>> UpdateUserAsync(User model);

        Task<BaseResponse<User>> InsertUserAsync(User model);

        Task DeleteUserAsync(string id);

        Task<BaseResponse<User>> LoginAsync(string username, string password);

        Task<BaseResponse<User>> GetUserByUsername(string username);

    }
}
