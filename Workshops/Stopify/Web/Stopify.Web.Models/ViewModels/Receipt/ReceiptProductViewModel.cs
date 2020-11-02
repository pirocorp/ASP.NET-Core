namespace Stopify.Web.Models.ViewModels.Receipt
{
    using Data.Models;
    using Services.Mapping;

    public class ReceiptProductViewModel : IMapFrom<Product>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
