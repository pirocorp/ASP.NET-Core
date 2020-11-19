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
    using static Common.GlobalConstants;

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
            if (model.Search is null)
            {
                model.Courses = await this.courseService.ActiveAsync<HomeIndexCourseListingModel>();
                return this.View(model);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (model.Page < 1)
            {
                model.Page = 1;
            }

            if (model.Search is SearchType.Articles)
            {
                var (collection, count) = await this.articlesService
                        .SearchAsync<HomeIndexArticleListingModel>(model.SearchText, ArticlesPageSize, model.Page);

                model.Articles = collection;
                model.TotalPages = count;
            }

            if (model.Search is SearchType.Courses)
            {
                var (collection, count) = await this.courseService
                    .SearchAsync<HomeIndexCourseListingModel>(model.SearchText, CoursesPageSize, model.Page);

                model.Courses = collection;
                model.TotalPages = count;
            }

            if (model.Search is SearchType.Users)
            {
                var (collection, count) = await this.userService
                    .SearchAsync<HomeIndexUserListingModel>(model.SearchText, UsersPageSize, model.Page);

                model.Users = collection;
                model.TotalPages = count;
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
