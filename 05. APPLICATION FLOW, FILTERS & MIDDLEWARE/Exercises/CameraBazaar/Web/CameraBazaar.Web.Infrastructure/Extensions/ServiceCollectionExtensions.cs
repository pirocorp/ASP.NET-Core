namespace CameraBazaar.Web.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add all services in IoC Container from target Assembly
        /// </summary>
        /// <param name="services">IoC Container</param>
        /// <param name="type">Type from target Assembly</param>
        /// <returns>Modified IoC Container</returns>
        public static IServiceCollection AddDomainServices(
            this IServiceCollection services, Type type)
        {
            Assembly
                .GetAssembly(type)
                .GetTypes()
                .Where(t => t.IsClass
                            && t.GetInterfaces()
                                .Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new 
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Implementation));
           
            return services;
        }
    }
}
