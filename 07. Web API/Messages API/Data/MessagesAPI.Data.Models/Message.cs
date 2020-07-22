namespace MessagesAPI.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string User { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
