namespace Stopify.Web.Models.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Mapping;
    using Services.Models;
    using ViewModels.ProductType;

    public class ProductEditInputModel : IMapFrom<Product>, IMapTo<ProductEditServiceModel>
    {
        public ProductEditInputModel()
        {
            this.Categories = new List<ProductTypeListingModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public IFormFile Picture { get; set; }

        public string PictureUri { get; set; }

        public DateTime ManufacturedOn { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public IEnumerable<ProductTypeListingModel> Categories { get; set; }

        public IEnumerable<SelectListItem> CategoriesListItems =>
            this.Categories
                .Select(r => new SelectListItem(r.Name, r.Id.ToString(), r.Name.Equals(this.TypeName)));
    }
}
