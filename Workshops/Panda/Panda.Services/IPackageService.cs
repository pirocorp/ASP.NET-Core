namespace Panda.Services
{
    using System.Threading.Tasks;
    using Models;

    public interface IPackageService
    {
        Task<string> CreateAsync(PackageCreateServiceModel model);
    }
}
