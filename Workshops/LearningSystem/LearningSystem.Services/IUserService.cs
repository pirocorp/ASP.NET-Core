namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<TOut> GetByUsernameAsync<TOut>(string userId);

        Task<(IEnumerable<TOut> collection, int count)> SearchAsync<TOut>(string filter, int pageSize, int page = 1);

        Task<byte[]> GetPdfCertificate(int courseId, string studentId);
    }
}