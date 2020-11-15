namespace LearningSystem.Services.Models.Admin.Blog
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.Common.DataConstants;

    public class CreateBlogArticleServiceModel
    {
        [Required]
        [MinLength(ArticleTitleMinLength)]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PublishedDate { get; set; }

        public string AuthorId { get; set; }
    }
}
