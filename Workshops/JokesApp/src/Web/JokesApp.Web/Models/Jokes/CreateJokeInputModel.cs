namespace JokesApp.Web.Models.Jokes
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CreateJokeInputModel
    {
        [Required]
        [MinLength(20)]
        public string Content { get; set; }

        [ValidCategoryId]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
    }
}
