namespace FrontEnd.Pages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using ConferenceDTO;
    using Services;

    public class SearchModel : PageModel
    {
        private readonly IApiClient apiClient;

        public SearchModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public string Term { get; set; }

        public List<SearchResult> SearchResults { get; set; }

        public async Task OnGetAsync(string term)
        {
            Term = term;
            SearchResults = await this.apiClient.SearchAsync(term);
        }
    }

}
