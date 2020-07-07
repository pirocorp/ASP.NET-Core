namespace BookLibrary.Web.Pages.Borrowers
{
    using System.ComponentModel.DataAnnotations;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;

    public class AddModel : BasePageModel
    {
        public AddModel(BookLibraryDbContext db) 
            : base(db)
        { }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        { }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var borrower = new Borrower()
            {
                Name = this.Input.Name,
                Address = this.Input.Address
            };

            this._db.Borrowers.Add(borrower);
            this._db.SaveChanges();

            return this.RedirectToPage("/Index");
        }

        public class InputModel
        {
            [Required]
            [MinLength(3)]
            public string Name { get; set; }

            [Required]
            [MinLength(3)]
            public string Address { get; set; }
        }
    }
}