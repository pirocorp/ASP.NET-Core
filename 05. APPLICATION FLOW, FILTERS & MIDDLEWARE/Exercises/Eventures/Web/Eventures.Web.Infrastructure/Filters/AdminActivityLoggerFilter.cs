namespace Eventures.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public class AdminActivityLoggerFilter: IActionFilter
    {
        private readonly ILogger<AdminActivityLoggerFilter> _logger;

        public AdminActivityLoggerFilter(
            ILogger<AdminActivityLoggerFilter> logger)
        {
            this._logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var name = context.HttpContext.User.Identity.Name;
            this._logger.LogInformation($"Administrator: {name} created new event");
        }
    }
}
