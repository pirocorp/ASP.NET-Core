﻿namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Models.Admin.Courses;

    public interface ICourseService
    {
        Task<int> CreateAsync(CreateCourseServiceModel model);

        Task<IEnumerable<TOut>> ActiveAsync<TOut>();

        Task<TOut> GetById<TOut>(int id);

        Task<bool> UserIsSignedInCourse(int courseId, ClaimsPrincipal user);

        Task<bool> ExistsAsync(int courseId);

        Task SignInUserAsync(int courseId, ClaimsPrincipal user);

        Task SignOutUserAsync(int courseId, ClaimsPrincipal user);

    }
}