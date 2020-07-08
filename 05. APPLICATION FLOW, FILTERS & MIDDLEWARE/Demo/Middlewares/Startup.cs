namespace Demo
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Middlewares;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Register custom middleware
            app.UseMiddleware<RedirectToGoogleIfNotHttpsMiddleware>();

            //Register predefined middleware
            app.UseStaticFiles();

            //Register another pipeline(chain) of middlewares for this route
            //All middlewares before this fork are in affect
            app.Map("/piro", app =>
            {
                app.Run(async (context) =>
                {
                    await context
                        .Response
                        .WriteAsync("Hello from another pipeline\n");
                });
            });

            //Register another pipeline(chain) of middlewares for this route
            //All middlewares before this fork are in affect
            app.Map("/exception", app =>
            {
                app.UseDeveloperExceptionPage();

                app.Run(async (context) =>
                {
                    throw new Exception();
                });
            });

            //Register another pipeline(chain) of middlewares for this route
            //All middlewares before this fork are in affect
            app.Map("/test", app =>
            {
                //Register in-line middleware
                app.Use(async (context, next) =>
                {
                    await context
                        .Response
                        .WriteAsync("BEFORE\n");

                    await next();

                    await context
                        .Response
                        .WriteAsync("After\n");
                });

                //Register in-line terminal middleware
                app.Run(async (context) =>
                {
                    await context
                        .Response
                        .WriteAsync("Hello from terminal(last) middleware\n");
                });
            });

            //Microsoft's Hello World Middleware
            app.UseWelcomePage();
        }
    }
}
