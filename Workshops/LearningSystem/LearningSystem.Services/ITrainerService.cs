namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Models.Trainers;

    public interface ITrainerService
    {
        Task<IEnumerable<TOut>> GetTrainingsAsync<TOut>(ClaimsPrincipal trainer);

        Task<bool> IsTrainerAsync(int courseId, ClaimsPrincipal trainer);  

        Task<IEnumerable<TOut>> GetAllStudentsInCourseAsync<TOut>(int courseId);

        Task<bool> Grade(GradeServiceModel model);
    }
}
