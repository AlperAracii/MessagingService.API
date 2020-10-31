using MessagingService.API.Data.Entities;
using MessagingService.API.Data.Repositories;
using MessagingService.API.Models.Response;
using MessagingService.API.Services.Users;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace MessagingService.API.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly BlockRepository _blockRepository;
        private readonly IUserService _userService;
        public AccountService(BlockRepository blockRepository, IUserService userService)
        {
            _blockRepository = blockRepository;
            _userService = userService;
        }

        public async Task<BaseResponse<BlockList>> BlockUserAsync(BlockList model)
        {
            var nullChecker = new ObjectId();
            var response = new BaseResponse<BlockList>();

            var user = await _userService.GetUserByUsername(model.UserName);
            var blockedUser = await _userService.GetUserByUsername(model.BlockedUserName);
            model.UserId = user.Data.Id;
            model.BlockedUserId = blockedUser.Data.Id;

            if (model.UserId != nullChecker || model.BlockedUserId != nullChecker)
            {
                var isBlocked = _blockRepository.IsBlocked(model);
                if (isBlocked.Result.Id == nullChecker)
                {
                    var result = await _blockRepository.InsertAsync(model);
                    if (result.Id != default)
                    {
                        response.Data = result;
                        return response;
                    }
                }
                response.Errors.Add("Kullanıcı daha önce blocklanmış!");
                return response;
            }
            response.Errors.Add("Block işlemi sırasında hata oluştu!");
            return response;
        }

        public async Task<BaseResponse<BlockList>> UnblockUserAsync(BlockList model)
        {
            var nullChecker = new ObjectId();
            var response = new BaseResponse<BlockList>();

            var user = await _userService.GetUserByUsername(model.UserName);
            var blockedUser = await _userService.GetUserByUsername(model.BlockedUserName);
            model.UserId = user.Data.Id;
            model.BlockedUserId = blockedUser.Data.Id;

            var isBlocked = _blockRepository.IsBlocked(model);

            if (isBlocked.Result.Id != nullChecker)
            {
                response.Data.Id = isBlocked.Result.Id;
                await _blockRepository.DeleteAsync(isBlocked.Result.Id.ToString());
                response.Message = "Kullanıcı unblock yapıldı.";
                return response;
            }
            response.Errors.Add("Kullanıcı blocklu değil!");
            return response;
        }

    }
}
