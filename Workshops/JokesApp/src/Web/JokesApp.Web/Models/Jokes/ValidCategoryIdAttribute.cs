namespace JokesApp.Web.Models.Jokes
{
    using System.ComponentModel.DataAnnotations;
    using Services.DataServices;

    public class ValidCategoryIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (ICategoriesService) validationContext
                .GetService(typeof(ICategoriesService));
            
            if (
                service is null || 
                !service.Exists((int) value))
            {
                return new ValidationResult("Invalid category");
            }

            return ValidationResult.Success;
        }
    }
}