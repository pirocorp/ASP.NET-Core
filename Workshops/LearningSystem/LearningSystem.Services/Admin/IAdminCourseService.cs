namespace LearningSystem.Services.Admin
{
    using System.Threading.Tasks;
    using Models.Admin.Courses;

    public interface IAdminCourseService
    {
        Task<int> CreateAsync(CreateCourseServiceModel model);
    }
}
