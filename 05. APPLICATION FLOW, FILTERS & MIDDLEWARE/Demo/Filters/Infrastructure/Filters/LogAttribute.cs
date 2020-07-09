namespace Filters.Infrastructure.Filters
{
    using Data;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;

    public class LogAttribute : ActionFilterAttribute
    {
        private readonly LogType _logType;

        public LogAttribute()
        {
            this._logType = LogType.Info;
        }

        public LogAttribute(LogType logType)
        {
            this._logType = logType;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var user = context.HttpContext.User.Identity.Name;
            var actionName = context.ActionDescriptor.DisplayName;

            var db = context
                .HttpContext
                .RequestServices
                .GetService<ApplicationDbContext>();
           //...TODO 
        }
    }
}
