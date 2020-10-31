namespace Stopify.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Stopify.Data;
    using Stopify.Data.Models;

    public class ProductTypeService : IProductTypeService
    {
        private readonly StopifyDbContext dbContext;

        public ProductTypeService(StopifyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistsAsync(string name)
            => await this.dbContext
                .ProductTypes
                .AnyAsync(t => t.Name.ToUpper().Equals(name.ToUpper()));

        public async Task<int> CreateAsync(string name)
        {
            var type = new ProductType()
            {
                Name = name
            };

            await this.dbContext.AddAsync(type);
            await this.dbContext.SaveChangesAsync();

            return type.Id;
        }

        public async Task<IEnumerable<TOut>> AllAsync<TOut>()
            => await this.dbContext
                .ProductTypes
                .To<TOut>()
                .ToListAsync();
    }
}
