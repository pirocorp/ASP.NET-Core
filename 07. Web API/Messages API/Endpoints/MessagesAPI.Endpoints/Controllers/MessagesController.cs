namespace MessagesAPI.Endpoints.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;

    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService messagesService;
        private readonly IUserService userService;

        public MessagesController(
            IMessagesService messagesService,
            IUserService userService)
        {
            this.messagesService = messagesService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var messages = await this.messagesService
                .AllAsync(23);

            return this.Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MessageCreateViewModel model)
        {
            var user = this.userService.GetByName(model.Username);

            await this.messagesService
                .CreateAsync(model.Content, user);

            return this.Ok();
        }
    }
}
