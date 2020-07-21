namespace MessagesAPI.Data.Models
{
    using System;

    public class Message
    {
        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public string User { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
