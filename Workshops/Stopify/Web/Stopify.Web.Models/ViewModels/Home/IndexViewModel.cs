namespace Stopify.Web.Models.ViewModels.Home
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ProductType;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Products = new List<ProductIndexViewModel>();
            this.Categories = new List<ProductTypeListingModel>();

            this.TypeId = 0;
            this.IsAscending = true;
        }

        public IEnumerable<ProductIndexViewModel> Products { get; set; }

        public int TypeId { get; set; }

        public IEnumerable<ProductTypeListingModel> Categories { get; set; }

        public IEnumerable<SelectListItem> CategoriesListItems =>
            this.Categories
                .Select(r => new SelectListItem(r.Name, r.Id.ToString()));

        public bool IsAscending { get; set; }

        public IEnumerable<SelectListItem> OrderListItems => new List<SelectListItem>()
        {
            new SelectListItem("Ascending", "true", true),
            new SelectListItem("Descending", "false", false)
        };
    }
}
