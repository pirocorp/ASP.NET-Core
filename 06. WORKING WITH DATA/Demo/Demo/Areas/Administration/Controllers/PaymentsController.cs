namespace Demo.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class PaymentsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
