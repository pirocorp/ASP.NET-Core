using System;

namespace Stopify.Infrastructure.Web
{
    using CloudinaryDotNet;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudinary(
            this IServiceCollection services,
            string cloudName,
            string apiKey,
            string apiSecret)
        {
            var cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
            var cloudinaryService = new Cloudinary(cloudinaryAccount);

            return services.AddSingleton(cloudinaryService);
        }
    }
}
