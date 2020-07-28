namespace CameraBazaar.Web.Controllers
{
    using Data.Models;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cameras;
    using Services;

    public class CamerasController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ICameraService cameraService;

        public CamerasController(
            UserManager<User> userManager,
            ICameraService cameraService)
        {
            this.userManager = userManager;
            this.cameraService = cameraService;
        }

        [Authorize]
        public IActionResult Add() => this.View();

        [HttpPost]
        [Authorize]
        //Don't name 'cameraModel' 'model'
        //if inside there is property with name Model
        //model binder will not bind it
        public IActionResult Add(AddCameraViewModel cameraModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(cameraModel);
            }

            var userId = this.userManager.GetUserId(this.User);

            this.cameraService
                .Create(
                    cameraModel.Make,
                    cameraModel.Model,
                    cameraModel.Price,
                    cameraModel.Quantity,
                    cameraModel.MinShutterSpeed,
                    cameraModel.MaxShutterSpeed,
                    cameraModel.MinISO,
                    cameraModel.MaxISO,
                    cameraModel.IsFullFrame,
                    cameraModel.VideoResolution,
                    cameraModel.LightMeterings,
                    cameraModel.Description,
                    cameraModel.ImageUrl,
                    userId);

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
