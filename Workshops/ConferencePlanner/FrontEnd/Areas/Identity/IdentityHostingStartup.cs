using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(FrontEnd.Areas.Identity.IdentityHostingStartup))]
namespace FrontEnd.Areas.Identity
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using FrontEnd.Data;

    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services
                    .AddDbContext<IdentityDbContext>(options =>
                        options
                            .UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddDefaultIdentity<User>(
                        options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>();
            });
        }
    }
}