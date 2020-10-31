namespace FrontEnd.Pages.Admin
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using ConferenceDTO;
    using Services;

    public class EditSessionModel : PageModel
    {
        private readonly IApiClient apiClient;

        public EditSessionModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        [BindProperty]
        public Session Session { get; set; }

        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(this.Message);

        public async Task OnGetAsync(int id)
        {
            var session = await this.apiClient.GetSessionAsync(id);
            this.Session = new Session
            {
                Id = session.Id,
                TrackId = session.TrackId,
                Title = session.Title,
                Abstract = session.Abstract,
                StartTime = session.StartTime,
                EndTime = session.EndTime
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }
            
            await this.apiClient.PutSessionAsync(this.Session);

            this.Message = "Session updated successfully!";

            return this.RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var session = await this.apiClient.GetSessionAsync(id);

            if (session != null)
            {
                await this.apiClient.DeleteSessionAsync(id);
            }

            this.Message = "Session deleted successfully!";

            return this.RedirectToPage("/Index");
        }
    }
}
