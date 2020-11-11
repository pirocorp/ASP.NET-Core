namespace JokesApp.Services.Models.Categories
{
    using Data.Models;
    using Mapping;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
