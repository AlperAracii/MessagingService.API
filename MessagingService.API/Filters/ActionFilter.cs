using MessagingService.API.Data.Repositories;
using MessagingService.API.Models;
using MessagingService.API.Services.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using System;
using System.Linq;

namespace MessagingService.API.Filters
{
    public class ActionFilter : IActionFilter
    {
        private readonly ILogService _logger;
        private readonly string _userId;

        public ActionFilter(ILogService logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userId = httpContextAccessor.HttpContext.Request.Headers["userId"];
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var pathItems = context.ActionDescriptor.RouteValues.ToList();
            var model = new AudithLogModel
            {
                CreatedOn = DateTime.Now,
                Message = context.ActionDescriptor.AttributeRouteInfo.Template,
                UserId = "test",
                ActionName = pathItems[0].Value,
                ControlerName = pathItems[1].Value
            };

            _logger.SaveAudithLog(model);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
