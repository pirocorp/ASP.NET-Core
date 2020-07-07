namespace BookLibrary.Web.Pages.Books
{
    using System;
    using System.Linq;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;

    public class ReturnModel : BasePageModel
    {
        public ReturnModel(BookLibraryDbContext db)
            : base(db)
        {

        }

        public string Title { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id is null)
            {
                return this.NotFound();
            }

            var book = this._db
                .Books
                .Find(id);

            if (book is null)
            {
                return this.NotFound();
            }

            var borrowedBook = this._db
                .BooksBorrowers
                .FirstOrDefault(b => b.BookId == id && b.EndDate == null);

            if (borrowedBook is null)
            {
                return this.NotFound();
            }

            this.Title = book.Title;

            borrowedBook.EndDate = DateTime.UtcNow;
            book.Status = BookStatus.AtHome;
            this._db.SaveChanges();

            return this.Page();
        }
    }
}