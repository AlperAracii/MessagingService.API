using MessagingService.API.Data.Entities;
using MessagingService.API.Data.Repositories;
using MessagingService.API.Filters;
using MessagingService.API.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.API.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly LoggingRepository _logger;
        public UserController(IUserService userService, LoggingRepository logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [ServiceFilter(typeof(ActionFilter))]
        [HttpGet("id")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] string id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Data);
        }

        [ServiceFilter(typeof(ActionFilter))]
        [HttpGet("username")]
        public async Task<IActionResult> GetUserByUsername([FromRoute] string username)
        {
            var response = await _userService.GetUserByUsername(username);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Data);
        }

        [ServiceFilter(typeof(ActionFilter))]
        [HttpGet("list")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var response = await _userService.GetAllUsersAsync();
            if (response.Any())
                return Ok(response);
            return BadRequest();
        }

        [ServiceFilter(typeof(ActionFilter))]
        [HttpPost]
        public async Task<IActionResult> InsertUserAsync(User model)
        {
            var response = await _userService.InsertUserAsync(model);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Data);
        }

        [ServiceFilter(typeof(ActionFilter))]
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(User model)
        {
            var response = await _userService.UpdateUserAsync(model);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Data);
        }

        [ServiceFilter(typeof(ActionFilter))]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] string id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }


    }
}
