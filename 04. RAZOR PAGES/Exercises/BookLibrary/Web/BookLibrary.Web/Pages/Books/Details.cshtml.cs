namespace BookLibrary.Web.Pages.Books
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    
    using Data;
    using Data.Models;
    using Extensions;
    using Microsoft.AspNetCore.Mvc;

    public class DetailsModel : BasePageModel
    {
        public DetailsModel(BookLibraryDbContext db)
            : base(db)
        {
        }

        public OutputModel Output { get; set; }

        public IActionResult OnGet(int id)
        {
            var bookModel = this._db.Books
                .Where(b => b.Id == id)
                .Select(b => new OutputModel()
                {
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    Description = b.Description,
                    ImageUrl = b.CoverImage,
                    Status = b.Status,
                })
                .FirstOrDefault();

            if (bookModel is null)
            {
                return this.NotFound();
            }

            this.Output = bookModel;
            return this.Page();
        }

        public class OutputModel
        {
            public string Title { get; set; }

            [Display(Name = "Author")]
            public string AuthorName { get; set; }

            public string Description { get; set; }

            public string ImageUrl { get; set; }

            public BookStatus Status { get; set; }

            public string StatusText => this.Status.FriendlyToString();

            public string StatusColor => this.Status.FriendlyToColor();
        }
    }
}