namespace BookLibrary.Web.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly BookLibraryDbContext _db;

        public IndexModel(BookLibraryDbContext db)
        {
            this._db = db;
        }

        public List<BookDisplayModel> Books { get; set; }

        public void OnGet()
        {
            this.Books = this._db.Books
                .OrderBy(b => b.Title)
                .Select(b => new BookDisplayModel()
                {
                    BookId = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    AuthorId = b.AuthorId,
                    Status = b.Status
                })
                .ToList();
        }

        public IActionResult OnPost()
        {
            return this.RedirectToPage("/Books/Add");
        }

        public class BookDisplayModel
        {
            public int BookId { get; set; }

            public string Title { get; set; }

            public int AuthorId { get; set; }

            public string AuthorName { get; set; }

            public BookStatus Status { get; set; }
        }
    }
}
