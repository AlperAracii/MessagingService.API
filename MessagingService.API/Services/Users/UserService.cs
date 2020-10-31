using MessagingService.API.Data.Entities;
using MessagingService.API.Data.Repositories;
using MessagingService.API.Models;
using MessagingService.API.Models.Request;
using MessagingService.API.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingService.API.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;        

        public UserService(UserRepository userRepository, LogginRepository logginRepository)
        {
            _userRepository = userRepository;
            _logginRepository = logginRepository;
        }

        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<BaseResponse<User>> GetUserByIdAsync(string id)
        {
            var response = new BaseResponse<User>();
            var document = await _userRepository.GetByIdAsync(id);
            if (document != null)
            {
                response.Data = document;
                return response;
            }
            response.Errors.Add($"{id}'li kullanıcı bulunamadı");
            return response;
        }

        public async Task<BaseResponse<User>> InsertUserAsync(User model)
        {
            var response = new BaseResponse<User>();
            var isExist = await GetUserByUsername(model.UserName);
            if (isExist.Data.UserName == null)
            {
                var result = await _userRepository.InsertAsync(model);
                if (result.Id != default)
                {
                    response.Data = result;
                    return response;
                }
                response.Errors.Add("Kullanıcı kayıt işlemi sırasında hata oluştu");
                return response;
            }
            response.Errors.Add("Kullanıcı adı daha önceden alınmış!");
            return response;
        }

        public async Task<BaseResponse<User>> UpdateUserAsync(User model)
        {
            var response = new BaseResponse<User>();
            var document = await _userRepository.UpdateAsync(Convert.ToString(model.Id), model);
            if (document == null)
            {
                response.Errors.Add("Kullanıcı güncelleme sırasında bir hata oluştu");
                return response;
            }
            response.Data = document;
            return response;
        }

        public async Task<BaseResponse<User>> LoginAsync(string username, string password)
        {
            var response = new BaseResponse<User>();
            var document = await _userRepository.GetByLoginInfo(username);
            if (document != null)
            {
                if (document.Password == password)
                {
                    response.Data = document;
                    response.Message = "Giriş Başarılı";
                    return response;
                }
                response.Errors.Add($"Şifre yanlış!");
                return response;
            }
            response.Errors.Add($"{username} adlı kullanıcı bulunamadı");
            return response;
        }

        public async Task<BaseResponse<User>> GetUserByUsername(string username)
        {
            var response = new BaseResponse<User>();
            var document = await _userRepository.GetByUserNameAsync(username);
            if (document != null)
            {
                response.Data = document;
                return response;
            }
            response.Errors.Add($"{username} adlı kullanıcı bulunamadı");
            return response;
        }
    }
}
