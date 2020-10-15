namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels;
    using ForumSystem.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;

    public class HomeController : BaseController
    {
        private const string SessionName = "Name";
        private const string TempDataName = "InfoMessage";

        private readonly ICategoryService categoriesService;
        private readonly ILogger<HomeController> logger;
        private readonly IDistributedCache distributedCache;

        public HomeController(
            ICategoryService categoriesService,
            ILogger<HomeController> logger,
            IDistributedCache distributedCache)
        {
            this.categoriesService = categoriesService;
            this.logger = logger;
            this.distributedCache = distributedCache;
        }

        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60 * 60 * 24)]
        public IActionResult Index()
        {
            this.TempData[TempDataName] = "Thank you for visiting home page.";
            this.HttpContext.Session.SetString(SessionName, "Piro");
            this.logger.LogWarning("Hi");

            var viewModel = new IndexViewModel()
            {
                Categories = this.categoriesService
                    .GetAll<IndexCategoryViewModel, string>(null, x => x.Name)
                    .ToList(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> CacheTest()
        {
            const string dataKey = "DateTimeAsString";

            var data = await this.distributedCache.GetStringAsync(dataKey);

            if (data is null)
            {
                data = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

                await this.distributedCache.SetStringAsync(
                    dataKey,
                    data,
                    new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    });
            }

            return this.Ok(data);
        }

        public IActionResult SessionTest()
        {
            var value = this.HttpContext.Session.GetString(SessionName);

            if (value is null)
            {
                value = "No session data";
            }

            return this.Ok(value);
        }

        public IActionResult TempDataTest()
        {
            var value = this.TempData[TempDataName];

            if (value is null)
            {
                value = "No temp data";
            }

            return this.Ok(value);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
