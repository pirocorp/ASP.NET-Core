namespace Panda.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure;
    using Models;

    public interface IPackagesService
    {
        Task<string> CreateAsync(PackageCreateServiceModel model);

        Task<IEnumerable<TOutput>> GetPackagesByStatusCodeAsync<TOutput>(ShipmentStatus status);

        Task<IEnumerable<TOutput>> GetPackagesByStatusCodeAsync<TOutput>(string statusId);

        Task<bool> ExistsAsync(string packageId);

        Task<bool> ChangeStatusAsync(string packageId, ShipmentStatus status);

        Task<bool> ChangeStatusAsync(string packageId, string statusId);

        Task<TOutput> GetByIdAsync<TOutput>(string packageId);

        Task<IEnumerable<TOutput>> GetAllPackagesInTheSystemAsync<TOutput>();

        Task<IEnumerable<TOutput>> GetAllUserPackagesAsync<TOutput>(string userId);
    }
}
