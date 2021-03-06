﻿namespace ForumSystem.Web
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    // using Microsoft.Extensions.Logging;
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://0.0.0.0:5000;https://0.0.0.0:5001");
                    webBuilder.UseKestrel();
                    webBuilder.UseIIS();
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(options =>
                {
                    // options.ClearProviders();
                });
    }
}
