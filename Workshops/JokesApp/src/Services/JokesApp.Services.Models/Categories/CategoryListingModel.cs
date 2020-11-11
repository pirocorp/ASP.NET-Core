namespace JokesApp.Services.Models.Categories
{
    using Data.Models;
    using Mapping;

    public class CategoryListingModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int JokesCount { get; set; }
    }
}
