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

        public void OnGet(int id)
        {
            var bookTitle = this._db
                .Books
                .Find(id)
                .Title;

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
            this.Output.BookId = id;
        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var bookBorrower = new BookBorrower()
            {
                BorrowerId = this.Input.BorrowerId,
                BookId = this.Input.BookId,
                StartDate = this.Input.StartDate
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

            [Display(Name = "Start Date")]
            public DateTime StartDate { get; set; }
        }
    }
}