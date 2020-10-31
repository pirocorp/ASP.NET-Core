namespace FrontEnd.Pages
{
    using System.Threading.Tasks;
    using ConferenceDTO;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Services;


    public class SpeakerModel : PageModel
    {
        private readonly IApiClient apiClient;

        public SpeakerModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public SpeakerResponse Speaker { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            this.Speaker = await this.apiClient.GetSpeakerAsync(id);

            if (this.Speaker == null)
            {
                return this.NotFound();
            }

            return this.Page();
        }
    }
}
