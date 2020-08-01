// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
namespace WebApi.Data.Models
{
    using WebApi.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
