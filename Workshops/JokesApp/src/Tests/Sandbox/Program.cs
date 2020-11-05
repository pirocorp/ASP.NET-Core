namespace Sandbox
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using JokesApp.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;

                await SandboxCode(serviceProvider);
            }
        }

        private static async Task<int> SandboxCode(IServiceProvider serviceProvider)
        {
            var sw = Stopwatch.StartNew();

            // TODO: Test code goes here (this is the sandbox :)

            Console.WriteLine("Hello from sandbox");
            var db = serviceProvider.GetService<JokesAppDbContext>();
            Console.WriteLine(db);

            Console.WriteLine(sw.Elapsed);
            return await Task.FromResult(0);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddDbContext<JokesAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
