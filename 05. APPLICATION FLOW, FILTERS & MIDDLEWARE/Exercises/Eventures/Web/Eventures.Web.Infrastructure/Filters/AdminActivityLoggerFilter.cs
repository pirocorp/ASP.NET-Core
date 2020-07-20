namespace Eventures.Web.Infrastructure.Filters
{
    using System;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public class AdminActivityLoggerFilter: IActionFilter
    {
        private readonly ILogger<AdminActivityLoggerFilter> logger;

        public AdminActivityLoggerFilter(
            ILogger<AdminActivityLoggerFilter> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var username = context.HttpContext.User.Identity.Name;

            var eventName = context.ModelState["Name"].AttemptedValue;
            var eventStart = context.ModelState["Start"].AttemptedValue;
            var eventEnd = context.ModelState["End"].AttemptedValue;

            var message = $"[{DateTime.UtcNow}] Administrator {username} create event {eventName} ({eventStart} / {eventEnd})";
            this.logger.LogInformation(message);
        }
    }
}
