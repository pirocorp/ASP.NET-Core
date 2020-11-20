namespace LearningSystem.Web.Infrastructure.Extensions
{
    using Data;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class SelectListItemExtensions
    {
        public static SelectListItem SelectCurrentGradeOption(this SelectListItem option, Grade? grade)
        {
            if (grade is null)
            {
                option.Selected = false;
                return option;
            }

            option.Selected = option.Text.ToLower().Equals(grade.ToString().ToLower());

            return option;
        }
    }
}
