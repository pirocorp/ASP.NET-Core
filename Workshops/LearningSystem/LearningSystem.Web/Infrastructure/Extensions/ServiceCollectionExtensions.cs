namespace LearningSystem.Web.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Services.Mapping;

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

        /// <summary>
        /// Add HtmlSanitizer in IServiceCollection (DI Container)
        /// </summary>
        /// <param name="services">IServiceCollection (DI Container)</param>
        /// <returns>IServiceCollection (DI Container) with added HtmlSanitizer</returns>
        public static IServiceCollection AddHtmlSanitizer(this IServiceCollection services)
        {
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedAttributes.Add("class");
            services.AddSingleton<IHtmlSanitizer>(sanitizer);

            return services;
        }

        /// <summary>
        /// Add AutoMapper in IServiceCollection (DI Container)
        /// </summary>
        /// <param name="services">IServiceCollection (DI Container)</param>
        /// <returns>IServiceCollection (DI Container) with added AutoMapper</returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly); // Configuration
            services.AddSingleton(AutoMapperConfig.MapperInstance); // Register Service

            return services;
        }
    }
}
