namespace JokesApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Category : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Joke> Jokes { get; set; }
    }
}
