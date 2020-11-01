using MessagingService.API.Data.Entities;
using MessagingService.API.Filters;
using MessagingService.API.Models.Request;
using MessagingService.API.Services.Account;
using MessagingService.API.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.API.Controllers
{
    [ServiceFilter(typeof(ActionFilter))]
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public AccountController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }

        [ServiceFilter(typeof(ActionFilter))]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync(RequestLoginModel request)
        {
            var response = await _userService.LoginAsync(request);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Data);
        }
        [ServiceFilter(typeof(ActionFilter))]
        [HttpPost("block")]
        public async Task<IActionResult> BlockUserAsync(RequestBlockModel request)
        {
            var response = await _accountService.BlockUserAsync(request);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Data);
        }

        [ServiceFilter(typeof(ActionFilter))]
        [HttpDelete("unblock")]
        public async Task<IActionResult> UnblockUserAsync(RequestBlockModel request)
        {
            var response = await _accountService.UnblockUserAsync(request);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Message);
        }
    }
}