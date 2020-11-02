namespace Stopify.Web.Models.ViewModels.Receipt
{
    using System;
    using System.Collections.Generic;
    using Data.Models;
    using Services.Mapping;

    public class ReceiptDetailsViewModel : IMapFrom<Order>
    {
        public ReceiptDetailsViewModel()
        {
            this.Products = new List<ReceiptProductViewModel>();
        }

        public string Id { get; set; }

        public string UserUserName { get; set; }

        public DateTime IssuedOn { get; set; }

        public List<ReceiptProductViewModel> Products { get; set; }
    }
}
