namespace Panda.App.Models.ViewModels.Home
{
    using Panda.Mapping;
    using Panda.Models;

    public class HomeIndexPackageViewModel : IMapFrom<Package>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string StatusName { get; set; }
    }
}
