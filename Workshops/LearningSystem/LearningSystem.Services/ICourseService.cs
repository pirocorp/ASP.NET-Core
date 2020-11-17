namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Admin.Courses;

    public interface ICourseService
    {
        Task<int> CreateAsync(CreateCourseServiceModel model);

        Task<IEnumerable<TOut>> ActiveAsync<TOut>();

        Task<TOut> GetById<TOut>(int id);
    }
}
