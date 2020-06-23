namespace CatsServer.Middlewares
{
    using System.Threading.Tasks;
    using CatServer.Infrastructure.Common;
    using Microsoft.AspNetCore.Http;

    public class HtmlContentTypeMiddleware
    {
        private readonly RequestDelegate _next;

        public HtmlContentTypeMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context
                .Response
                .Headers
                .Add(HttpHeader.ContentType, "text/html");

            return this._next(context);
        }
    }
}
