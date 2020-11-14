namespace LearningSystem.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        Task<IEnumerable<TOut>> AllAsync<TOut>();
    }
}
