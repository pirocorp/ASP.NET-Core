namespace BookLibrary.Web.Pages.Books
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Mvc;

    public class StatusModel : BasePageModel
    {
        public StatusModel(BookLibraryDbContext db) 
            : base(db)
        {
            this.Output = new OutputModel();
        }

        public OutputModel Output { get; set; }

        public IActionResult OnGet(int id)
        {
            var book = this._db.Books.Find(id);

            if (book is null)
            {
                return this.NotFound();
            }

            this.Output.BookTitle = book.Title;

            this.Output.Listings = this._db
                .BooksBorrowers
                .Where(b => b.BookId == id)
                .Select(b => new ListingModel()
                {
                    BorrowerName = b.Borrower.Name,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate
                })
                .ToList();

            return this.Page();
        }

        public class OutputModel
        {
            public string BookTitle { get; set; }
            
            public IEnumerable<ListingModel> Listings { get; set; }
        }

        public class ListingModel
        {
            public string BorrowerName { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime? EndDate { get; set; }

            public string EndDateToString 
                => this.EndDate?.ToString("dd/MM/yyyy HH:mm")
                   ?? "It's still taken";
        }
    }
}