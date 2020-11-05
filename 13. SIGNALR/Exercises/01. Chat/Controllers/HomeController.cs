namespace _01._Chat.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(this.View());
        }
    }
}
