namespace FrontEnd.Services
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using ConferenceDTO;

    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> AddAttendeeAsync(Attendee attendee)
        {
            var response = await this.httpClient.PostAsJsonAsync($"/api/attendees", attendee);
             
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();
             
            return true;
        }

        public async Task<AttendeeResponse> GetAttendeeAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var response = await this.httpClient.GetAsync($"/api/attendees/{name}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<AttendeeResponse>();
        }

        public async Task<SessionResponse> GetSessionAsync(int id)
        {
            var response = await this.httpClient.GetAsync($"/api/sessions/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<SessionResponse>();
        }

        public async Task<List<SessionResponse>> GetSessionsAsync()
        {
            var response = await this.httpClient.GetAsync("/api/sessions");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<SessionResponse>>();
        }

        public async Task DeleteSessionAsync(int id)
        {
            var response = await this.httpClient.DeleteAsync($"/api/sessions/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task<SpeakerResponse> GetSpeakerAsync(int id)
        {
            var response = await this.httpClient.GetAsync($"/api/speakers/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<SpeakerResponse>();
        }

        public async Task<List<SpeakerResponse>> GetSpeakersAsync()
        {
            var response = await this.httpClient.GetAsync("/api/speakers");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<SpeakerResponse>>();
        }

        public async Task PutSessionAsync(Session session)
        {
            var response = await this.httpClient.PutAsJsonAsync($"/api/sessions/{session.Id}", session);

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<SearchResult>> SearchAsync(string query)
        {
            var term = new SearchTerm
            {
                Query = query
            };

            var response = await this.httpClient.PostAsJsonAsync($"/api/search", term);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<SearchResult>>();
        }
    }
}
