namespace BackEnd
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers();

            services.AddSwaggerGen(options => {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo {Title = "Conference Planner API", Version = "v1"});

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint(
                    "/swagger/v1/swagger.json", 
                    "Conference Planner API v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // When GET request is send to / will automatically redirect to /swagger
                endpoints.MapGet("/", context => { 
                    context.Response.Redirect("/swagger/");
                    return Task.CompletedTask;
                });

                endpoints.MapControllers();
            });
        }
    }
}
