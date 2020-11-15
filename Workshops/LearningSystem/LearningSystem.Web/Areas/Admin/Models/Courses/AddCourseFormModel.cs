namespace LearningSystem.Web.Areas.Admin.Models.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Mapping;
    using Services.Models.Admin.Courses;
    using static Data.Common.DataConstants;

    public class AddCourseFormModel : IMapFrom<Course>, IMapTo<CreateCourseServiceModel>, IValidatableObject
    {
        [Required]
        [MinLength(CourseNameMinLength)]
        [MaxLength(CourseNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CourseDescriptionMaxLength)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Trainer")]
        public string TrainerId { get; set; }

        public IEnumerable<SelectListItem> Trainers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate < DateTime.UtcNow)
            {
                yield return new ValidationResult("Start date should be in the future.", new []{ nameof(this.StartDate )});
            }

            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("Start date should be before end date.", new []{ nameof(this.StartDate), nameof(this.EndDate) });
            }
        }
    }
}
