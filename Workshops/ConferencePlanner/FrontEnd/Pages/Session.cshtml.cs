namespace FrontEnd.Pages
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using ConferenceDTO;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class SessionModel : PageModel
    {
        private readonly IApiClient apiClient;

        public SessionModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public SessionResponse Session { get; set; }

        public int? DayOffset { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            this.Session = await this.apiClient.GetSessionAsync(id);

            if (this.Session == null)
            {
                return this.RedirectToPage("/Index");
            }

            var allSessions = await this.apiClient.GetSessionsAsync();

            var startDate = allSessions.Min(s => s.StartTime?.Date);

            this.DayOffset = this.Session.StartTime?.Subtract(startDate ?? DateTimeOffset.MinValue).Days;

            return this.Page();
        }
    }
}
