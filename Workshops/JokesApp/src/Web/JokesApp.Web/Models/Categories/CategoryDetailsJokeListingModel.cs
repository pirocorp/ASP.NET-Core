namespace JokesApp.Web.Models.Categories
{
    using Data.Models;
    using Services.Mapping;

    public class CategoryDetailsJokeListingModel : IMapFrom<Joke>
    {
        public string Content { get; set; }
    }
}
