namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<TOut> GetByUsernameAsync<TOut>(string userId);

        Task<IEnumerable<TOut>> SearchAsync<TOut>(string filter = "");
    }
}