namespace Panda.App.Models.ViewModels.Receipt
{
    using System;

    using Panda.Mapping;
    using Panda.Models;

    public class ReceiptIndexViewModel : IMapFrom<Receipt>
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public string RecipientUserName { get; set; }
    }
}
