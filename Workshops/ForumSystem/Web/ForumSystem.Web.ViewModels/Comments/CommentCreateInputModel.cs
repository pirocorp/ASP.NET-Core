namespace ForumSystem.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentCreateInputModel
    {
        [Range(1, int.MaxValue)]
        public int PostId { get; set; }

        [Range(0, int.MaxValue)]
        public int ParentId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
