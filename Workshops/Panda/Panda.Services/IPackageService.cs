namespace Panda.Services
{
    using System.Threading.Tasks;

    public interface IPackageService
    {
        Task<string> CreateAsync(string description, double weight, string shippingAddress);
    }
}
