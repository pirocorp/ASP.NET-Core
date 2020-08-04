namespace ForumSystem.Web.ViewModels.Categories
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;

    public class PostInCategoryViewModel : IMapFrom<Post>
    {
        private const string PATTERN = @"<[^>]*>";

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ShortContent
        {
            get
            {
                var content = Regex.Replace(
                    this.Content,
                    PATTERN,
                    string.Empty);

                content = WebUtility.HtmlDecode(content);

                return content.Length > 300
                    ? content.Substring(0, 300) + "..."
                    : content;
            }
        }
    }
}
