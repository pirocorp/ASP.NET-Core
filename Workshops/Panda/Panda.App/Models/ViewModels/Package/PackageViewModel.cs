// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
namespace Panda.App.Models.ViewModels.Package
{
    using Panda.Mapping;
    using Panda.Models;

    public class PackageViewModel : IMapFrom<Package>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string RecipientUserName { get; set; }
    }
}
