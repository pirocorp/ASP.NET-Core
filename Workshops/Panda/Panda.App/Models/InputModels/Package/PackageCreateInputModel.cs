namespace Panda.App.Models.InputModels.Package
{
    using System.Collections.Generic;
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

        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string RecipientId { get; set; }

        public IEnumerable<PandaUserDropDownViewModel> Recipients { get; set; }

        public IEnumerable<SelectListItem> SelectListRecipients =>
            this.Recipients
                .Select(r => new SelectListItem(r.UserName, r.Id));
    }
}
