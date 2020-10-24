namespace Panda.App.Models.ViewModels.Receipt
{
    using System;

    using Panda.Mapping;
    using Panda.Models;

    public class ReceiptDetailsViewModel : IMapFrom<Receipt>
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public string RecipientId { get; set; }

        public string RecipientUserName { get; set; }

        public string PackageDescription { get; set; }

        public double PackageWeight { get; set; }

        public string PackageShippingAddress { get; set; }
    }
}
