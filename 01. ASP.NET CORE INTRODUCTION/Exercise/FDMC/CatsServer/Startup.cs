namespace CatsServer
{
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Middlewares.Extensions;

    public class Startup
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <remarks>For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940</remarks>
        /// <param name="services">IoC Container</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options => 
                options.UseSqlServer("Server=PIRO\\SQLEXPRESS2019;Database=CatsServerDb;Integrated Security=True;"));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <remarks>Non Terminal middlewares accepts next as parameter for handler function</remarks>
        /// <param name="app">Application Builder</param>
        /// <param name="env">Web Host Environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Apply database migrations middleware -> custom middleware
            app
                .UseDatabaseMigration();

            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .UseDatabaseErrorPage();
            }


            app
                .UseStaticFiles() //Static files - terminal middleware
                .UseHtmlContentType() //Non-terminal middleware -> custom middleware
                .UseRequestHandlers() //Used for several app.MapWhen
                .UseNotFoundHandler(); //Terminal middleware
        }
    }
}
