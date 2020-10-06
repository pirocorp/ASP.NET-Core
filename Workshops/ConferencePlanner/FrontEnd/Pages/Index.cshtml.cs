namespace FrontEnd.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    using ConferenceDTO;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class IndexModel : PageModel
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly ILogger<IndexModel> logger;
        private readonly IApiClient apiClient;

        public IndexModel(IApiClient apiClient, ILogger<IndexModel> logger)
        {
            this.logger = logger;
            this.apiClient = apiClient;
        }

        public IEnumerable<IGrouping<DateTimeOffset?, SessionResponse>> Sessions { get; set; }

        public IEnumerable<(int Offset, DayOfWeek? DayofWeek)> DayOffsets { get; set; }

        public int CurrentDayOffset { get; set; }

        public bool IsAdmin { get; set; }

        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(this.Message);

        public async Task OnGet(int day = 0)
        {
            this.IsAdmin = this.User.IsAdmin();

            this.CurrentDayOffset = day;

            var sessions = await this.apiClient.GetSessionsAsync();

            var startDate = sessions.Min(s => s.StartTime?.Date);

            var offset = 0;
            this.DayOffsets = sessions
                .Select(s => s.StartTime?.Date)
                .Distinct()
                .OrderBy(d => d)
                // ReSharper disable once VariableHidesOuterVariable
                .Select(day => (offset++, day?.DayOfWeek));

            var filterDate = startDate?.AddDays(day);

            this.Sessions = sessions
                .Where(s => s.StartTime?.Date == filterDate)
                .OrderBy(s => s.TrackId)
                .GroupBy(s => s.StartTime)
                .OrderBy(g => g.Key);
        }
    }
}
