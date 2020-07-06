namespace BookLibrary.Web.Pages
{
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public abstract class BasePageModel : PageModel
    {
        protected readonly BookLibraryDbContext _db;

        protected BasePageModel(BookLibraryDbContext db)
        {
            this._db = db;
        }
    }
}
