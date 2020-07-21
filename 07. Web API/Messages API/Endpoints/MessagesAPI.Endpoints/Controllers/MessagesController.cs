namespace MessagesAPI.Endpoints.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Data;

    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var messages = await this.messagesService
                .AllAsync();

            return this.Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MessageCreateViewModel model)
        {
            await this.messagesService
                .CreateAsync(model.Content, model.User);

            return this.Ok();
        }
    }
}
