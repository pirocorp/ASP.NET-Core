﻿namespace Stopify.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IProductService
    {
        Task<string> CreateAsync(ProductCreateServiceModel model);

        Task<string> EditAsync(ProductEditServiceModel model);

        Task<bool> DeleteAsync(string productId);

        Task<IEnumerable<TOut>> AllNotSoldAsync<TOut>(int typeId = 0, bool isAscending = true);

        Task<TOut> GetByIdAsync<TOut>(string id);

        Task<bool> ProductIsSoldAsync(string id);

        Task<bool> ExistsAsync(string productId);

        Task<bool> RemoveProductFromOrder(string productId);

        Task<bool> AddProductInOrder(string productId, string orderId);
    }
}
