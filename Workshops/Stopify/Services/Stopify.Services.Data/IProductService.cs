﻿namespace Stopify.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Stopify.Data.Models;

    public interface IProductService
    {
        Task<string> CreateAsync(ProductCreateServiceModel model);

        Task<IEnumerable<TOut>> AllNotSoldAsync<TOut>(int typeId = 0, bool isAscending = true);

        Task<TOut> GetByIdAsync<TOut>(string id);

        Task<Product> GetByIdAsync(string id);

        Task<bool> ProductIsSoldAsync(string id);
    }
}
