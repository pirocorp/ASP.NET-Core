namespace CatsServer.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Cat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CatsServerValidationConstraints.Cat.StringMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(CatsServerValidationConstraints.Cat.AgeMinValue, CatsServerValidationConstraints.Cat.AgeMaxValue)]
        public int Age { get; set; }

        [Required]
        [MaxLength(CatsServerValidationConstraints.Cat.StringMaxLength)]
        public string Breed { get; set; }

        [Required]
        [MaxLength(CatsServerValidationConstraints.Cat.ImageUrlMaxLength)]
        public string ImageUrl { get; set; }
    }
}
