﻿namespace Panda.App
{
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Panda.App.Models;
    using Panda.Data;
    using Panda.Data.Seeding;
    using Panda.Mapping;
    using Panda.Models;
    using Panda.Services;
    using Panda.Services.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PandaDbContext>(options =>
            {
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            services
                .AddDefaultIdentity<PandaUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<PandaRole>()
                .AddEntityFrameworkStores<PandaDbContext>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = consent => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews(options =>
                {
                    // Auto validation of CSRF tokens
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }) // MVC
                .AddRazorRuntimeCompilation();

            services.AddRazorPages();

            services.AddSingleton(this.Configuration);

            // Application Services
            services.AddTransient<IPackagesService, PackagesService>();
            services.AddTransient<IStatusesService, StatusesService>();
            services.AddTransient<IReceiptsService, ReceiptsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig
                .RegisterMappings(
                    typeof(ErrorViewModel).GetTypeInfo().Assembly,
                    typeof(PackageCreateServiceModel).GetTypeInfo().Assembly);

            if (env.IsDevelopment())
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var pandaDbContext = serviceScope.ServiceProvider.GetRequiredService<PandaDbContext>();
                    pandaDbContext.Database.Migrate();
                    new ApplicationDbContextSeeder()
                        .SeedAsync(pandaDbContext, serviceScope.ServiceProvider)
                        .GetAwaiter()
                        .GetResult();
                }

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                    .MapControllerRoute(
                        name: "areaRoute",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
