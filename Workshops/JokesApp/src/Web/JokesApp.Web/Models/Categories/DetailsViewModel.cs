namespace JokesApp.Web.Models.Categories
{
    using System;
    using System.Collections.Generic;

    public class DetailsViewModel
    {
        private const double PageSize = 10;

        public DetailsViewModel()
        {
            this.Jokes = new List<CategoryDetailsJokeListingModel>();
        }

        public CategoriesDetailsViewModel Category { get; set; }

        public IEnumerable<CategoryDetailsJokeListingModel> Jokes { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling(this.Category.JokesCount / PageSize);
    }
}
