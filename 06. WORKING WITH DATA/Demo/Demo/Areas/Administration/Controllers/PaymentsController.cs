namespace Demo.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = "admin")]
    public class PaymentsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
