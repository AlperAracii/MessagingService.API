using MessagingService.API.Data.Entities;
using MessagingService.API.Services.Message;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.API.Controllers
{
    [ApiController]
    [Route("api/mesagges")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageAsync(Messages model)
        {
            var response = await _messageService.SendMessage(model);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Data);
        }

        [HttpGet("get-messages")]
        public async Task<IActionResult> GetMessagesAsync(string userName)
        {
            var response = await _messageService.GetMyMessagesAsync(userName);
            if (response.Errors.Any())
                return BadRequest(response.Errors);
            return Ok(response.Data);
        }

    }
}
