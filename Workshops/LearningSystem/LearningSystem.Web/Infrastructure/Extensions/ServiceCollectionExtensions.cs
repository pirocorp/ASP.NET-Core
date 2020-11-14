namespace LearningSystem.Web.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Extension method for configuring Learning System Identity
        /// </summary>
        /// <typeparam name="TUser">Learning System Identity User Model</typeparam>
        /// <param name="services">IServiceCollection extension</param>
        /// <returns></returns>
        public static IdentityBuilder AddLearningSystemIdentity<TUser>(this IServiceCollection services)
            where TUser : IdentityUser 
        {
            return services.AddIdentity<TUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequiredLength = 6;

                    options.User.RequireUniqueEmail = true;

                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                });
        }

        /// <summary>
        /// Add all services in IServiceCollection Container from the target Assembly
        /// </summary>
        /// <param name="services">IServiceCollection Container</param>
        /// <param name="type">Type from target Assembly</param>
        /// <returns>IServiceCollection Container with added services</returns>
        public static IServiceCollection AddDomainServices(this IServiceCollection services, Type type)
        {
            Assembly
                .GetAssembly(type)
                ?.GetTypes()
                .Where(t => t.IsClass
                            && t.GetInterfaces()
                                .Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new 
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(s => s.Interface is not null)
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Implementation));
           
            return services;
        }
    }
}
