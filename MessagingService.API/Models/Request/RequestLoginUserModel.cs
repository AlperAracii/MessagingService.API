using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.API.Models.Request
{
    public class RequestLoginUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
