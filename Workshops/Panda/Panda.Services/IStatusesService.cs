namespace Panda.Services
{
    using System.Threading.Tasks;

    public interface IStatusesService
    {
        Task<string> GetPackageStatusIdByNameAsync(string name);

        Task<bool> ExistsAsync(string statusId);
    }
}
