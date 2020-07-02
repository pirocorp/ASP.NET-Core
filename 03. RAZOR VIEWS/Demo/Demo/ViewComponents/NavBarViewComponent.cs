namespace Demo.ViewComponents
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels.NavBar;

    public class NavBarViewComponent : ViewComponent
    {
        private readonly IYearsService _yearsService;

        public NavBarViewComponent(IYearsService yearsService)
        {
            this._yearsService = yearsService;
        }

        public IViewComponentResult Invoke(int count)
        {
            var model = new NavBarViewModel()
            {
                Years = this._yearsService.GetLastYears(count).Reverse(),
            };

            return this.View(model);
        }
    }
}
