namespace Stopify.Web.Models.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ViewModels.ProductType;

    public class ProductCreateInputModel
    {
        public ProductCreateInputModel()
        {
            this.Categories = new List<ProductTypeListingModel>();
            this.ManufacturedOn = DateTime.UtcNow;
        }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Range(0, 1_000_000)]
        public decimal Price { get; set; }

        [Display(Name = "Manufactured On")]
        public DateTime ManufacturedOn { get; set; }

        public IFormFile Picture { get; set; }

        public int TypeId { get; set; }

        public IEnumerable<ProductTypeListingModel> Categories { get; set; }

        public IEnumerable<SelectListItem> CategoriesListItems =>
            this.Categories
                .Select(r => new SelectListItem(r.Name, r.Id.ToString()));
    }
}
