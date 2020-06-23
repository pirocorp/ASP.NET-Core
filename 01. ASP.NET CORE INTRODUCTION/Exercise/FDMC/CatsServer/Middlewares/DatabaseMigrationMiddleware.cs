namespace CatsServer.Middlewares
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class DatabaseMigrationMiddleware
    {
        private readonly RequestDelegate _next;

        public DatabaseMigrationMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context
                .RequestServices
                .GetService<CatsDbContext>()
                .Database
                .Migrate();

            return this._next(context);
        }
    }
}
