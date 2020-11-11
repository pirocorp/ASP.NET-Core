namespace JokesApp.Web.Models.Categories
{
    using System.Collections.Generic;
    using Services.Models.Categories;

    public class CategoryIndexViewModel
    {
        public CategoryIndexViewModel()
        {
            this.Categories = new List<CategoryListingModel>();
        }

        public IEnumerable<CategoryListingModel> Categories { get; set; }
    }
}
