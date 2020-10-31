using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.API.Models.Response
{
    public class BaseResponse<T> where T : new()
    {
        public BaseResponse()
        {
            Data = new T();
            Errors = new List<string>();
        }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public bool HasError => Errors.Any();
    }
}
