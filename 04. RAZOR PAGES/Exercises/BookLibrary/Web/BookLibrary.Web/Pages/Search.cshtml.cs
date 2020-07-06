namespace BookLibrary.Web.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class SearchModel : PageModel
    {
        private readonly BookLibraryDbContext _db;

        public SearchModel(BookLibraryDbContext db)
        {
            this._db = db;

            this.Output = new OutputModel();
        }

        public OutputModel Output { get; set; }

        public void OnGet(string searchTerm)
        {
            var results = new List<ListingModel>();

            var authors = this._db
                .Authors
                .Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()))
                .Select(a => new ListingModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Source = "author",
                })
                .ToList();

            var books = this._db
                .Books
                .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()))
                .Select(b =>  new ListingModel()
                {
                    Id = b.Id,
                    Name = b.Title,
                    Source = "book",
                })
                .ToList();

            results.AddRange(authors);
            results.AddRange(books);

            this.Output.Listings = results;
            this.Output.SearchTerm = searchTerm;
        }

        public class OutputModel
        {
            public IEnumerable<ListingModel> Listings { get; set; }

            public string SearchTerm { get; set; }
        }

        public class ListingModel
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Source { get; set; }
        }
    }
}