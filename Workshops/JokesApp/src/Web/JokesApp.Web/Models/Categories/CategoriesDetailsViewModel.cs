namespace JokesApp.Web.Models.Categories
{
    using System.Collections.Generic;
    using Data.Models;
    using Services.Mapping;

    public class CategoriesDetailsViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public IEnumerable<CategoryDetailsJokeListingModel> Jokes { get; set; }
    }
}
