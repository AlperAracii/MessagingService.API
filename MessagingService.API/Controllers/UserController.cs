﻿using MessagingService.API.Data.Entities;
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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            if (response != default)
                return Ok(response);
            return BadRequest();
        }

        [HttpGet("username")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var response = await _userService.GetUserByUsername(username);
            if (response != default)
                return Ok(response);
            return BadRequest();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var response = await _userService.GetAllUsersAsync();
            if (response.Any())
                return Ok(response);
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> InsertUserAsync(User model)
        {
            var response = await _userService.InsertUserAsync(model);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Data);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(User model)
        {
            var response = await _userService.UpdateUserAsync(model);
            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }
            return Ok(response.Data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }


    }
}
