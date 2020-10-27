namespace Stopify.Web.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ProductTypeCreateInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
