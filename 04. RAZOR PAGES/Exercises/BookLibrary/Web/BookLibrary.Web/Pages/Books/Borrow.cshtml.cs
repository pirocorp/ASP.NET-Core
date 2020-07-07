namespace BookLibrary.Web.Pages.Books
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class BorrowModel : BasePageModel
    {
        public BorrowModel(BookLibraryDbContext db) 
            : base(db)
        {
            this.Output = new OutputModel();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public OutputModel Output { get; set; }

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

            var bookTitle = book.Title;

            var borrowers = this._db
                .Borrowers
                .AsQueryable()
                .Select(b => new SelectListItem()
                {
                    Text = b.Name,
                    Value = b.Id.ToString()
                })
                .ToList();

            this.Output.Title = bookTitle;
            this.Output.Borrowers = borrowers;
            this.Output.BookId = id.Value;

            return this.Page();
        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var inputBook = this._db
                .Books
                .Find(this.Input.BookId);

            if (inputBook is null)
            {
                return this.NotFound();
            }

            var inputBorrower = this._db
                .Borrowers
                .Find(this.Input.BorrowerId);

            if (inputBorrower is null)
            {
                return this.NotFound();
            }

            var bookBorrower = new BookBorrower()
            {
                BorrowerId = this.Input.BorrowerId,
                BookId = this.Input.BookId,
                StartDate = DateTime.UtcNow,
            };

            var book = this._db.Books.Find(this.Input.BookId);
            book.Status = BookStatus.Borrowed;

            this._db.BooksBorrowers.Add(bookBorrower);
            this._db.SaveChanges();

            return this.RedirectToPage("/Index");
        }

        public class OutputModel
        {
            public int BookId { get; set; }

            public string Title { get; set; }

            public IEnumerable<SelectListItem> Borrowers { get; set; }
        }

        public class InputModel
        {
            public int BookId { get; set; }

            [Display(Name = "Borrower")]
            public int BorrowerId { get; set; }
        }
    }
}