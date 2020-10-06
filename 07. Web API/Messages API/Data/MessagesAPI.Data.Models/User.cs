namespace MessagesAPI.Data.Models
{
    using System;
    using System.Collections.Generic;

    // ReSharper disable CollectionNeverUpdated.Global
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Messages = new HashSet<Message>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
