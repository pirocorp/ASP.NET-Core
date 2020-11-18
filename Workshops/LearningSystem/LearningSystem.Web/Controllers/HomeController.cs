namespace LearningSystem.Web.Controllers
{
    using System.Threading.Tasks;
    using Models.Courses;
    using Services;
    using System.Diagnostics;
    using Infrastructure.Enum;
    using LearningSystem.Web.Models;

    using Microsoft.AspNetCore.Mvc;
    using Models.Home;
    using Services.Blog;


    public class HomeController : Controller
    {
        private readonly IBlogArticlesService articlesService;
        private readonly ICourseService courseService;
        private readonly IUserService userService;

        public HomeController(
            IBlogArticlesService articlesService,
            ICourseService courseService,
            IUserService userService)
        {
            this.articlesService = articlesService;
            this.courseService = courseService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index(HomeIndexViewModel model)
        {
            // TODO Pagination

            if (model.Search is null)
            {
                model.Courses = await this.courseService.ActiveAsync<HomeIndexCourseListingModel>();
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (model.Search is SearchType.Articles)
            {
                model.Articles = await this.articlesService.SearchAsync<HomeIndexArticleListingModel>(model.SearchText);
            }

            if (model.Search is SearchType.Courses)
            {
                model.Courses = await this.courseService.SearchAsync<HomeIndexCourseListingModel>(model.SearchText);
            }

            if (model.Search is SearchType.Users)
            {
                model.Users = await this.userService.SearchAsync<HomeIndexUserListingModel>(model.SearchText);
            }

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
