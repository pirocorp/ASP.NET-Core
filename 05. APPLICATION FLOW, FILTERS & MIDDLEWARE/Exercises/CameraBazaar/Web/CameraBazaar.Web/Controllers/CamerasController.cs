namespace CameraBazaar.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CamerasController : Controller
    {
        [Authorize]
        public IActionResult Add() => this.View();
    }
}
