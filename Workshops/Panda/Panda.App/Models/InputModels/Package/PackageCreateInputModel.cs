namespace Panda.App.Models.InputModels.Package
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using Panda.Mapping;
    using Panda.Services.Models;

    public class PackageCreateInputModel : IMapTo<PackageCreateServiceModel>
    {
        public PackageCreateInputModel()
        {
            this.Recipients = new List<PandaUserDropDownViewModel>();
        }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Description { get; set; }

        [Range(0.1, 50_000)]
        public double Weight { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string ShippingAddress { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public IEnumerable<PandaUserDropDownViewModel> Recipients { get; set; }

        public IEnumerable<SelectListItem> SelectListRecipients =>
            this.Recipients
                .Select(r => new SelectListItem(r.UserName, r.Id));
    }
}
