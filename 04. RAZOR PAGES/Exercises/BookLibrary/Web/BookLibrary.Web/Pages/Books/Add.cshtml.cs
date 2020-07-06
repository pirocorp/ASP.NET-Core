namespace BookLibrary.Web.Pages.Books
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using Data;
    using Data.Models;

    public class AddModel : BasePageModel
    {
        public AddModel(BookLibraryDbContext db)
            : base(db)
        { }

        [BindProperty]
        public InputModel Input { get; set; }

        public IActionResult OnGet()
        {
            return this.Page();
        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var author = this._db
                .Authors
                .FirstOrDefault(a => a.Name == this.Input.AuthorName);

            if (author is null)
            {
                author = new Author()
                {
                    Name = this.Input.AuthorName
                };
            }

            var book = new Book()
            {
                Title = this.Input.Title,
                Description = this.Input.Description,
                CoverImage = this.Input.ImageUrl,
                Author = author
            };

            this._db.Books.Add(book);
            this._db.SaveChanges();

            return this.RedirectToPage("Details", "Books", new {id = book.Id});
        }

        public class InputModel
        {
            [Required]
            public string Title { get; set; }

            [Required]
            public string Description { get; set; }

            [Required]
            [Display(Name = "Image URL")]
            [DataType(DataType.Url)]
            public string ImageUrl { get; set; }

            [Required]
            [Display(Name = "Author")]
            public string AuthorName { get; set; }
        }
    }
}