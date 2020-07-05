namespace BookLibrary.Web.Pages.Books
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class AddModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {

        }


        public class InputModel
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public string CoverImage { get; set; }

            public string Author { get; set; }
        }
    }
}