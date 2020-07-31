namespace MessagesAPI.Endpoints.Models
{
    using System.ComponentModel.DataAnnotations;

    public class MessageCreateViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
