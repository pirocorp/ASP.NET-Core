namespace LearningSystem.Web.Models.Trainers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Mapping;

    public class TrainersStudentsViewModel : IMapFrom<Course>
    {
        public TrainersStudentsViewModel()
        {
            this.Students = new List<TrainersStudentsStudentListingModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime EndDate { get; set; }

        [IgnoreMap]
        public TrainersStudentsInputModel Input { get; set; }

        [IgnoreMap]
        public IEnumerable<TrainersStudentsStudentListingModel> Students { get; set; }

        public IEnumerable<SelectListItem> GradesDropDown => Enum.GetValues<Grade>()
            .Select(e => new SelectListItem(e.ToString(), $"{(int)e}"));
    }
}
