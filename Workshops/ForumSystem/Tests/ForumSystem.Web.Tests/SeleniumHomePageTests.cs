namespace ForumSystem.Web.Tests
{
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;
    using Xunit;

    public class SeleniumHomePageTests
    {
        private readonly SeleniumServerFactory<Startup> serverFactory;
        private readonly RemoteWebDriver browser;

        public SeleniumHomePageTests()
        {
            this.serverFactory = new SeleniumServerFactory<Startup>();

            // this.serverFactory.CreateClient();
            var options = new ChromeOptions();
            options.AddArguments("--headless", "--ignore-certificate-errors");

            this.browser = new RemoteWebDriver(options);
        }

        [Fact]
        public void HomePageShouldHaveH1Tag()
        {
            this.browser
                .Navigate()
                .GoToUrl(this.serverFactory.RootUri + "/Home/Index");

            Assert.Contains(
                "Welcome to",
                this.browser.FindElementByCssSelector("h1").Text);
        }
    }
}
