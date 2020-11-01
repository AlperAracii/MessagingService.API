using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.API.Models.Request
{
    public class RequestBlockModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string BlockedUserName { get; set; }


    }
}
