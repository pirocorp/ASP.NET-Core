namespace MessagesAPI.Endpoints.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Applies migrations
        /// </summary>
        /// <typeparam name="T">Migration context</typeparam>
        /// <param name="app">IApplicationBuilder</param>
        /// <returns>IApplicationBuilder object</returns>
        public static IApplicationBuilder UseDatabaseMigration<T>(this IApplicationBuilder app)
            where T : DbContext
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<T>();
            context.Database.Migrate();

            return app;
        }
    }
}
