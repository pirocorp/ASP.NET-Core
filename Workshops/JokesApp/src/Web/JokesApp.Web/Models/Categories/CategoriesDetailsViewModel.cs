namespace JokesApp.Web.Models.Categories
{
    using Data.Models;
    using Services.Mapping;

    public class CategoriesDetailsViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int JokesCount { get; set; }
    }
}
