using MessagingService.API.Data.Entities;
using MessagingService.API.Filters;
using MessagingService.API.Models.Request;
using MessagingService.API.Services.Message;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.API.Controllers
{
    [ServiceFilter(typeof(ActionFilter))]
    [ApiController]
    [Route("api/mesagges")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [ServiceFilter(typeof(ActionFilter))]
        [HttpPost]
        public async Task<IActionResult> SendMessageAsync(RequestMessageModel request)
        {
            var response = await _messageService.SendMessage(request);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Data);
        }

        [ServiceFilter(typeof(ActionFilter))]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetMessagesAsync([FromRoute] string username)
        {
            var response = await _messageService.GetMyMessagesAsync(username);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Data);
        }

    }
}
