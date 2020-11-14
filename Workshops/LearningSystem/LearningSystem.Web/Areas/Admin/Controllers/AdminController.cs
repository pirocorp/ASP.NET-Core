namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public abstract class AdminController : Controller
    {
    }
}
