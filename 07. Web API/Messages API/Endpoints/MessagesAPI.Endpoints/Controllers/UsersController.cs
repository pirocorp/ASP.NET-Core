namespace MessagesAPI.Endpoints.Controllers
{
    using System;
    
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            this.userService.Register(model.Username, model.Password);

            return this.Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel model)
        {
            var result = this.userService.Login(model.Username, model.Password);

            if (result is null)
            {
                return this.BadRequest();
            }

            return this.Ok(result);
        }
    }
}
