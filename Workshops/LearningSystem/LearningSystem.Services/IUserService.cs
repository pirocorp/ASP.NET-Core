namespace LearningSystem.Services
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<TOut> GetByUsernameAsync<TOut>(string userId);
    }
}