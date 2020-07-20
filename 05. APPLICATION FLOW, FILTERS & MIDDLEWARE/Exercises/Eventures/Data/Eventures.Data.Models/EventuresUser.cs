namespace Eventures.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class EventuresUser : IdentityUser
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [Required]
        [MaxLength(10)]
        public string Ucn { get; set; }
    }
}
