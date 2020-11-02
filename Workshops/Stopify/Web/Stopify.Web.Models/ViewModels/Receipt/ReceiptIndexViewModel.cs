namespace Stopify.Web.Models.ViewModels.Receipt
{
    using System.Collections.Generic;

    public class ReceiptIndexViewModel 
    {
        public ReceiptIndexViewModel()
        {
            this.Orders = new List<ReceiptOrderListingModel>();
        }

        public IEnumerable<ReceiptOrderListingModel> Orders { get; set; }
    }
}
