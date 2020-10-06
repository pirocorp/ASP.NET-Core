namespace FrontEnd.Data
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
