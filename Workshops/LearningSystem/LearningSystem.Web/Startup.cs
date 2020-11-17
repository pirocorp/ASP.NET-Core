namespace LearningSystem.Web
{
    using Data;
    using Data.Seeding;
    using Infrastructure.Extensions;

    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Services;

    using static Common.GlobalConstants;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LearningSystemDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddLearningSystemIdentity<User>()
                .AddEntityFrameworkStores<LearningSystemDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews(options =>
                {
                    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>(); // CSRF Token
                })
                .AddRazorRuntimeCompilation();

            services.AddRazorPages();

            // Routing Configuration
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            // Filters
            services.AddDatabaseDeveloperPageExceptionFilter(); // This filter is used to produce database developer exception page

            // Automapper 
            services.AddAutoMapper();

            // HtmlSanitizer
            services.AddHtmlSanitizer(); // Or sanitizer can be added to service and service registered in DI container (better abstraction)

            // Add application services.
            services.AddDomainServices(typeof(IService));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // This two method are added in .net 5 instead of app.UseDatabaseErrorPage();
                // In services DeveloperPageExceptionFilter must be registered
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();

                app.UseDatabaseMigration<LearningSystemDbContext>();
            }
            else
            {
                app.UseExceptionHandler("/home/error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/home/error");

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<LearningSystemDbContext>();
                dbContext.Database.Migrate();
                new LearningSystemDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "profile",
                    pattern: "users/{username}", 
                    new
                    {
                        controller = "Users",
                        action = "Profile"
                    }
                );

                endpoints.MapControllerRoute(
                    name: "blog",
                    pattern: "{area:exists}/{controller=articles}/{id}/{title}", 
                    new { action = "details" });

                endpoints.MapControllerRoute(
                    name: "courses",
                    pattern: "{controller=courses}/{id}/{title}", 
                    new { action = "details" });

                endpoints.MapControllerRoute(
                    name:"areas", 
                    pattern:"{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
