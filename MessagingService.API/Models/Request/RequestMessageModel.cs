using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.API.Models.Request
{
    public class RequestMessageModel
    {
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public string FromId { get; set; }
        public string Message { get; set; }
    }
}
