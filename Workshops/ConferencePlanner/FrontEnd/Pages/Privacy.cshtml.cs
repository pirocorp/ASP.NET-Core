using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Pages
{
    public class PrivacyModel : PageModel
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly ILogger<PrivacyModel> logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            this.logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
