namespace JokesApp.Services.Models.Jokes
{
    using Data.Models;
    using Mapping;

    public class JokeDetailsViewModel : IMapFrom<Joke>
    {
        public string Content { get; set; }

        public string CategoryName { get; set; }
    }
}
