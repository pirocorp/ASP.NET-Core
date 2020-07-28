namespace CameraBazaar.Web.Infrastructure.Filters
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Filters;
    
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Task.Run(async () =>
            {
                await using var writer = new StreamWriter("logs.txt", true);

                var date = DateTime.UtcNow;
                var ipAddress = context.HttpContext.Connection.RemoteIpAddress;
                var userName = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
                var controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];

                var logMessage = $"{date} - {ipAddress} - {userName} - {controller}.{action}";

                if (context.Exception != null)
                {
                    var exceptionType = context.Exception.GetType().Name;
                    var exceptionMessage = context.Exception.Message;

                    logMessage =
                        $"[!] {logMessage} - {exceptionType} - {exceptionMessage}";
                }

                await writer.WriteLineAsync(logMessage);
            })
                .GetAwaiter()
                .GetResult();
        }
    }
}
