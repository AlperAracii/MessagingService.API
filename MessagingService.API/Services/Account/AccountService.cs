using MessagingService.API.Data.Entities;
using MessagingService.API.Data.Repositories;
using MessagingService.API.Models.Request;
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

        public async Task<BaseResponse<BlockList>> BlockUserAsync(RequestBlockModel request)
        {
            var nullChecker = new ObjectId();
            var response = new BaseResponse<BlockList>();

            var blockedUser = await _userService.GetUserByUsername(request.BlockedUserName);
            var entitiy = new BlockList
            {
                UserId = new ObjectId(request.UserId),
                BlockedUserId = blockedUser.Data.Id,
                UserName = request.UserName,
                BlockedUserName = request.BlockedUserName
            };

            if (entitiy.UserId != nullChecker && entitiy.BlockedUserId != nullChecker)
            {
                var isBlocked = _blockRepository.IsBlocked(entitiy);
                if (isBlocked.Result == null)
                {
                    var result = await _blockRepository.InsertAsync(entitiy);
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

        public async Task<BaseResponse<BlockList>> UnblockUserAsync(RequestBlockModel request)
        {
            var response = new BaseResponse<BlockList>();

            var blockedUser = await _userService.GetUserByUsername(request.BlockedUserName);
            var entitiy = new BlockList
            {
                UserId = new ObjectId(request.UserId),
                BlockedUserId = blockedUser.Data.Id,
                UserName = request.UserName,
                BlockedUserName = request.BlockedUserName
            };

            var isBlocked = _blockRepository.IsBlocked(entitiy);

            if (isBlocked.Result != null)
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
