using MessagingService.API.Data.Entities;
using MessagingService.API.Models.Request;
using MessagingService.API.Services.Account;
using MessagingService.API.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.API.Controllers
{
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

        [HttpGet("login")]
        public async Task<IActionResult> LoginUserAsync(string username, string password)
        {
            var response = await _userService.LoginAsync(username, password);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Message);
        }

        [HttpPost("block")]
        public async Task<IActionResult> BlockUserAsync(BlockList model)
        {
            var response = await _accountService.BlockUserAsync(model);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Message);
        }

        [HttpDelete("unblock")]
        public async Task<IActionResult> UnblockUserAsync(BlockList model)
        {
            var response = await _accountService.UnblockUserAsync(model);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Message);
        }
    }
}