namespace ForumSystem.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;

    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<PostInCategoryViewModel> ForumPosts { get; set; } = new List<PostInCategoryViewModel>();

        public int NextPage => Math.Min(this.CurrentPage + 1, this.PagesCount);

        public int PreviousPage => Math.Max(1, this.CurrentPage - 1);

        public string NextState => this.GetState(this.NextPage);

        public string PrevState => this.GetState(this.PreviousPage);

        public string CurrentState(int i)
            => this.CurrentPage == i ? "active" : string.Empty;

        private string GetState(int newPageState)
            => newPageState == this.CurrentPage ? "disabled" : string.Empty;
    }
}
