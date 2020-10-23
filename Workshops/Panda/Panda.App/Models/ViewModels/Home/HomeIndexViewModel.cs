namespace Panda.App.Models.ViewModels.Home
{
    using System.Collections.Generic;

    public class HomeIndexViewModel
    {
        public IEnumerable<HomeIndexPackageViewModel> Pending { get; set; }

        public IEnumerable<HomeIndexPackageViewModel> Shipped { get; set; }

        public IEnumerable<HomeIndexPackageViewModel> Delivered { get; set; }
    }
}
