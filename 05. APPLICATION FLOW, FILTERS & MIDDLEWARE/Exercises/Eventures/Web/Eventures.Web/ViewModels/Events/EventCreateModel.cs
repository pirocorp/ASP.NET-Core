namespace Eventures.Web.ViewModels.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EventCreateModel
    {
        [Required]
        [Display(Name = "Name")]
        [MinLength(10)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required]
        [Display(Name = "Start")]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name = "End")]
        public DateTime End { get; set; }

        [Required]
        [Display(Name = "Total Tickets")]
        [Range(0, int.MaxValue, ErrorMessage = "Total tickets must be positive number.")]
        public int TotalTickets { get; set; }

        [Required]
        [Display(Name = "Total Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive number.")]
        public decimal Price { get; set; }
    }
}
