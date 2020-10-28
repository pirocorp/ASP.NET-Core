namespace Stopify.Web
{
    using System.Globalization;
    using System.Reflection;
    using CloudinaryDotNet;
    using Data.Seeding;
    using Infrastructure.Web;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Models;
    using Services;
    using Services.Data;
    using Services.Mapping;
    using Stopify.Services.Messaging;

    using Stopify.Data;
    using Stopify.Data.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StopifyDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<StopifyUser, IdentityRole>(IdentityOptionsProvider.GetIdentityOptions)
                .AddEntityFrameworkStores<StopifyDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI(); // Not scaffolded pages from identity will work too

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews();
            services.AddRazorPages();

            // External Services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddCloudinary(
                this.Configuration["Cloudinary:CloudName"],
                this.Configuration["Cloudinary:ApiKey"],
                this.Configuration["Cloudinary:ApiSecret"]);

            // Application Services
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductTypeService, ProductTypeService>();
            services.AddTransient<IPictureService, PictureService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            AutoMapperConfig
                .RegisterMappings(
                    typeof(ErrorViewModel).GetTypeInfo().Assembly);

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider
                    .GetRequiredService<StopifyDbContext>();

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseDatabaseErrorPage();
                    
                    dbContext.Database.Migrate();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                new StopifyDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name : "areas",
                    pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
