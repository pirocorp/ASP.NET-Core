namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Common.GlobalConstants;

    [Area(AdministratorArea)]
    [Authorize(Roles = AdministratorRole)]
    public abstract class AdminController : Controller
    {
    }
}
