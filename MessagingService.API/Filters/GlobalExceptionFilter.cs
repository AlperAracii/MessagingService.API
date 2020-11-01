using MessagingService.API.Data.Repositories;
using MessagingService.API.Models;
using MessagingService.API.Services.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Net;

namespace MessagingService.API.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogService _logger;
        private readonly string _userId;

        public GlobalExceptionFilter(ILogService logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userId = httpContextAccessor.HttpContext.Request.Headers["userId"];
        }

        public override void OnException(ExceptionContext context)
        {
            var model = new LogModel
            {
                UserId = "test",
                Message = context.Exception.Message,
            };

            _logger.SaveExceptionLog(model);

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Result = new JsonResult(new List<string>() {"Sistemsel bir hata oluştu - 500"});
        }
    }
}
