namespace JokesApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Joke : BaseModel<int>
    {
        [Required]
        public string Content { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
