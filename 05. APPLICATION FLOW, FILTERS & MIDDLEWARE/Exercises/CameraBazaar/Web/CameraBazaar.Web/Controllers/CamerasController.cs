namespace CameraBazaar.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cameras;
    using Services;

    public class CamerasController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICameraService _cameraService;

        public CamerasController(
            UserManager<User> userManager,
            ICameraService cameraService)
        {
            this._userManager = userManager;
            this._cameraService = cameraService;
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

            var userId = this._userManager.GetUserId(this.User);

            this._cameraService
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
