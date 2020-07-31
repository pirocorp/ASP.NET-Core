using System;

namespace WebApi.Services.Models
{
    using Common.Mapping;
    using Data.Models;

    public class CreateArticleServiceModel : IMapTo<Article>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
