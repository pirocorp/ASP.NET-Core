namespace Panda.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IPackageService
    {
        Task<string> CreateAsync(PackageCreateServiceModel model);

        Task<IEnumerable<TOutput>> GetPackagesByStatusCodeAsync<TOutput>(string statusId);

        Task<bool> ExistsAsync(string packageId);

        Task<bool> ChangeStatusAsync(string packageId, string statusId);

        Task<TOutput> GetByIdAsync<TOutput>(string packageId);

        Task<IEnumerable<TOutput>> GetAllPackagesInTheSystemAsync<TOutput>();
    }
}
