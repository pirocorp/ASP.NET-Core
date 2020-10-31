namespace Stopify.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
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
                PictureUri = await this.pictureService.UploadPictureAsync(model.Picture),
            };

            await this.dbContext.AddAsync(product);
            await this.dbContext.SaveChangesAsync();

            return product.Id;
        }

        public async Task<IEnumerable<TOut>> AllNotSoldAsync<TOut>(int typeId, bool isAscending)
        {
            var productsQuery = this.dbContext.Products.AsQueryable();

            if (typeId > 0)
            {
                productsQuery = productsQuery
                    .Where(p => p.TypeId == typeId);
            }

            if (isAscending)
            {
                productsQuery = productsQuery
                    .OrderBy(p => p.Price);
            }
            else
            {
                productsQuery = productsQuery
                    .OrderByDescending(p => p.Price);
            }

            return await productsQuery
                .Where(p => p.OrderId.Equals(null))
                .To<TOut>()
                .ToListAsync();
        }

        public async Task<TOut> GetByIdAsync<TOut>(string id)
            => await this.dbContext.Products
                .Where(p => p.Id.Equals(id))
                .To<TOut>()
                .FirstOrDefaultAsync();

        public async Task<Product> GetByIdAsync(string id)
            => await this.dbContext.Products
                .FirstOrDefaultAsync(p => p.Id.Equals(id));

        public async Task<bool> ProductIsSoldAsync(string id)
            => (await this.dbContext.Products
                .FirstOrDefaultAsync(p => p.Id.Equals(id))).OrderId != null;
    }
}