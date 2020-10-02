namespace ForumSystem.Services.Data.Tests
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using ForumSystem.Web;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;
    using Xunit.Abstractions;

    public class HomePageTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public HomePageTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task HomePageShouldHaveH1Title()
        {
            var serverFactory = new WebApplicationFactory<Startup>();
            var client = serverFactory.CreateClient();

            var response = await client.GetAsync("/");
            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains("<h1", responseAsString);

            var stopWatch = Stopwatch.StartNew();

            response = await client.GetAsync("/");
            await response.Content.ReadAsStringAsync();

            stopWatch.Stop();
            this.testOutputHelper.WriteLine(stopWatch.Elapsed.ToString());
        }
    }
}
