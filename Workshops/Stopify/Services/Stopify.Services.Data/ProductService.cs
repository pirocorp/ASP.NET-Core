namespace Stopify.Services.Data
{
    using System.Threading.Tasks;
    using Models;
    using Stopify.Data;
    using Stopify.Data.Models;

    public class ProductService : IProductService
    {
        private readonly StopifyDbContext dbContext;
        private readonly IPictureService pictureService;

        public ProductService(
            StopifyDbContext dbContext,
            IPictureService pictureService
            )
        {
            this.dbContext = dbContext;
            this.pictureService = pictureService;
        }

        public async Task<string> CreateAsync(ProductCreateServiceModel model)
        {
            var product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                ManufacturedOn = model.ManufacturedOn,
                TypeId = model .TypeId,
                PictureUri = await this.pictureService.UploadPicture(model.Picture),
            };

            await this.dbContext.AddAsync(product);
            await this.dbContext.SaveChangesAsync();

            return product.Id;
        }
    }
}