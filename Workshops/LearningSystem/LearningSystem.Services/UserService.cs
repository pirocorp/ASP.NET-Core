namespace LearningSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Data;
    using Data.Models;
    using Mapping;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly LearningSystemDbContext dbContext;
        private readonly IPdfGenerator pdfGenerator;

        public UserService(
            LearningSystemDbContext dbContext,
            IPdfGenerator pdfGenerator)
        {
            this.dbContext = dbContext;
            this.pdfGenerator = pdfGenerator;
        }

        public async Task<TOut> GetByUsernameAsync<TOut>(string userId)
            => await this.dbContext.Users
                .Where(u => u.Id.Equals(userId))
                .To<TOut>(new{ studentId = userId })
                .SingleOrDefaultAsync();

        public async Task<(IEnumerable<TOut> collection, int count)> SearchAsync<TOut>(string filter, int pageSize, int page = 1)
        {
            var query = this.dbContext.Users.Select(x => x);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToLower();

                query = query.Where(x
                    => x.UserName.Contains(filter)
                       || x.Email.Contains(filter)
                       || x.Name.Contains(filter));
            }

            var count = (int)Math.Ceiling((query.Count() / (double) pageSize));

            var skip = (page - 1) * pageSize;

            var collection =  await query
                .OrderBy(x => x.UserName)
                .Skip(skip)
                .Take(pageSize)
                .To<TOut>()
                .ToListAsync();

            return (collection, count);
        }

        public async Task<byte[]> GetPdfCertificate(int courseId, string studentId)
        {
            var studentInCourse = await this.dbContext.FindAsync<StudentCourse>(studentId, courseId);

            if (studentInCourse is null)
            {
                return null;
            }

            var certificateInfo = await this.dbContext.Courses
                .Where(c => c.Id.Equals(courseId))
                .Select(c => new
                {
                    CourseName = c.Name,
                    CourseStartDate = c.StartDate,
                    CourseEndDate = c.EndDate,
                    StudentName = c.Students
                        .Where(s => s.StudentId.Equals(studentId))
                        .Select(s => s.Student.Name)
                        .FirstOrDefault(),
                    StudentGrade = c.Students
                        .Where(s => s.StudentId.Equals(studentId))
                        .Select(s => s.Grade)
                        .FirstOrDefault(),
                    TrainerName = c.Trainer.Name
                })
                .FirstOrDefaultAsync();

            var html = string.Format(
                GlobalConstants.PdfCertificateFormat, 
                certificateInfo.CourseName,
                certificateInfo.CourseStartDate.ToString("dd MMMM yyyy"),
                certificateInfo.CourseEndDate.ToString("dd MMMM yyyy"),
                certificateInfo.StudentName,
                certificateInfo.StudentGrade,
                certificateInfo.TrainerName);

            var pdfContents = this.pdfGenerator.GeneratePdfFromHtml(html);
            return pdfContents;
        }
    }
}
