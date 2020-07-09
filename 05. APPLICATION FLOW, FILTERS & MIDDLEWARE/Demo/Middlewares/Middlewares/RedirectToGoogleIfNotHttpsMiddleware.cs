namespace Middlewares.Middlewares
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Middleware redirects to google if request is coming
    /// not by https, but by http protocol
    /// </summary>
    /// <remarks>DI is supported in InvokeAsync Method</remarks>
    public class RedirectToGoogleIfNotHttpsMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Middleware constructor 
        /// </summary>
        /// <param name="next">Next middleware Func&lt;HttpContext, Func&lt;Task&gt;, Task></param>
        
        public RedirectToGoogleIfNotHttpsMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        /// <summary>
        /// Body of the middleware
        /// </summary>
        /// <param name="context">Http Context</param>
        /// <remarks>After the context supports DI and can
        /// be injected any registered service</remarks>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.IsHttps)
            {
                context.Response.StatusCode = 307;
                context.Response.Headers["Location"] = "https://google.com";
            }
            else
            {
                await this._next(context);
            }
        }
    }
}
