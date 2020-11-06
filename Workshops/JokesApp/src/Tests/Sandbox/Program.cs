namespace Sandbox
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AngleSharp;
    using AngleSharp.Dom;
    using JokesApp.Data;
    using JokesApp.Data.Models;
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

        private static async Task SandboxCode(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<JokesAppDbContext>();
            
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            var batchSize = 100;

            for (var batchStart = 701; batchStart < 48598; batchStart+= batchSize)
            {
                var documents = new List<Task<IDocument>>();

                for (var i = batchStart; i < batchStart + batchSize; i++)
                {
                    Console.WriteLine($"Searching for joke with id: {i}");

                    var url = $"https://fun.dir.bg/vic_open.php?id={i}";

                    documents.Add(context.OpenAsync(url));
                }

                await Task.WhenAll(documents); // Parallel Execution (somewhat)

                foreach (var document in documents)
                {
                    var contentId = "#newsbody";
                    var contentElement = (await document).QuerySelector(contentId);

                    var categorySelector = "div.openvic-left > p.tag-links-left > a";
                    var categoryElement = (await document).QuerySelector(categorySelector);

                    if (contentElement is null
                        || categoryElement is null)
                    {
                        continue;
                    }

                    var jokeContent = contentElement.TextContent;
                    var category = categoryElement.TextContent;

                    var categoryId = await db.Categories
                        .Where(c => c.Name.Equals(category))
                        .Select(c => c.Id)
                        .FirstOrDefaultAsync();

                    if (categoryId == 0)
                    {
                        var categoryEntity = new Category()
                        {
                            Name = category
                        };

                        await db.Categories.AddAsync(categoryEntity);
                        await db.SaveChangesAsync();

                        categoryId = categoryEntity.Id;
                    }

                    var joke = new Joke()
                    {
                        CategoryId = categoryId,
                        Content = jokeContent
                    };

                    await db.Jokes.AddAsync(joke);
                    await db.SaveChangesAsync();

                    Console.WriteLine($"Add joke {joke.Id} to DB");
                }
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddDbContext<JokesAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
