namespace Filters.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RandomModel
    {
        [Required]
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
