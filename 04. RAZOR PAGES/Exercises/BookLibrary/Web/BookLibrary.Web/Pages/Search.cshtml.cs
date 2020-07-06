namespace BookLibrary.Web.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Mvc;

    public class SearchModel : BasePageModel
    {
        public SearchModel(BookLibraryDbContext db)
            : base(db)
        {
            this.Output = new OutputModel();
        }

        public OutputModel Output { get; private set; }

        public IActionResult OnGet(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return this.RedirectToPage("Index");
            }

            var results = new List<OutputModel.ListingModel>();

            var authors = this._db
                .Authors
                .Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()))
                .Select(a => new OutputModel.ListingModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Source = "author",
                })
                .ToList();

            var books = this._db
                .Books
                .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()))
                .Select(b =>  new OutputModel.ListingModel()
                {
                    Id = b.Id,
                    Name = b.Title,
                    Source = "book",
                })
                .ToList();

            results.AddRange(authors);
            results.AddRange(books);

            results = results
                .OrderBy(l => l.Name)
                .ToList();

            this.Output.Listings = results;
            this.Output.SearchTerm = searchTerm;

            return this.Page();
        }

        public class OutputModel
        {
            public IEnumerable<ListingModel> Listings { get; set; }

            public string SearchTerm { get; set; }

            public class ListingModel
            {
                public int Id { get; set; }

                public string Name { get; set; }

                public string Source { get; set; }
            }
        }
    }
}