namespace BookLibrary.Web.Pages.Authors
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using Data;
    using Data.Models;

    public class DetailsModel : BasePageModel
    {
        public DetailsModel(BookLibraryDbContext db)
            : base(db)
        {
            this.Output = new OutputModel();
        }

        public OutputModel Output { get; set; }

        public IActionResult OnGet(int id)
        {
            var author = this._db.Authors.Find(id);

            if (author is null)
            {
                return this.NotFound();
            }

            this.Output.AuthorName = author.Name;

            this.Output.Books = this._db
                .Books
                .Where(b => b.AuthorId == id)
                .Select(b => new BookDisplayModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Status = b.Status,
                })
                .ToList();

            return this.Page();
        }

        public class OutputModel
        {
            public List<BookDisplayModel> Books { get; set; }

            public string AuthorName { get; set; }
        }

        public class BookDisplayModel
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public BookStatus Status { get; set; }
        }
    }
}