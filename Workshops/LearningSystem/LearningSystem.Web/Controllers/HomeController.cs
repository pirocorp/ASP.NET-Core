namespace LearningSystem.Web.Controllers
{
    using System.Threading.Tasks;
    using Models.Courses;
    using Services;
    using System.Diagnostics;
    using LearningSystem.Web.Models;

    using Microsoft.AspNetCore.Mvc;


    public class HomeController : Controller
    {
        private readonly ICourseService courseService;

        public HomeController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel()
            {
                Courses = await this.courseService.ActiveAsync<HomeIndexCourseListingModel>(),
            };

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
