namespace ForumSystem.Data.Models
{
    using ForumSystem.Data.Common.Models;

    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class Comment : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }
    }
}
