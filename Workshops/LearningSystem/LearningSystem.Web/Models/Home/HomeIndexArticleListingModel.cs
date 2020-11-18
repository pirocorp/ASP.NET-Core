namespace LearningSystem.Web.Models.Home
{
    using System;
    using Data.Models;
    using Services.Mapping;
    using static Common.GlobalConstants;

    public class HomeIndexArticleListingModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishedDate { get; set; }

        public string AuthorName { get; set; }

        public string DisplayContent => this.Content.Length <= ContentDemoLength 
            ? this.Content
            : this.Content
                .Substring(0, ContentDemoLength)
                .TrimEnd('/')
                .TrimEnd('<')+ "...";

        public int PublishedDaysAgo => this.DaysAgo();

        private int DaysAgo()
        {
            var days = (DateTime.UtcNow - this.PublishedDate).Days;

            if (DateTime.UtcNow.DayOfYear > this.PublishedDate.DayOfYear)
            {
                days += 1;
            }

            return days;
        }
    }
}
