namespace CameraBazaar.Data.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Cameras = new HashSet<Camera>();
        }

        public ICollection<Camera> Cameras { get; set; }
    }
}
