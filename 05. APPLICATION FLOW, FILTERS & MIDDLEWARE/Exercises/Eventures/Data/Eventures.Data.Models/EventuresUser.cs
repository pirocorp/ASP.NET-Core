namespace Eventures.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class EventuresUser : IdentityUser
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Ucn { get; set; }
    }
}
