namespace Panda.App.Models.ViewModels.Package
{
    using Panda.Mapping;
    using Panda.Models;

    public class PackageAcquireModel : IMapFrom<Package>
    {
        public string Id { get; set; }

        public double Weight { get; set; }

        public string RecipientId { get; set; }
    }
}
