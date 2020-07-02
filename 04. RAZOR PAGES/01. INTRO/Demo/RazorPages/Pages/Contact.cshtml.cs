namespace RazorPages.Pages
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    
    /// <summary>
    /// Razor Pages support Dependency injection
    /// </summary>
    public class ContactModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public string Info { get; set; }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (this.ModelState.IsValid)
            {
                this.Info = "Your message was successfully submitted";
            }
            else
            {
                this.Info = "All fields are required.";
            }
        }

        public class InputModel
        {
            [Required]
            [MinLength(3)]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            [MinLength(6)]
            public string Email { get; set; }

            [Required]
            [MinLength(2)]
            public string Title { get; set; }

            [Required]
            [MinLength(10)]
            public string Content { get; set; }
        }
    }
}