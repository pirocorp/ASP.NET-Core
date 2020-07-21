namespace MessagesAPI.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MessagesAPI.Data;
    using MessagesAPI.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class MessagesService : IMessagesService
    {
        private readonly MessagesDbContext db;

        public MessagesService(MessagesDbContext db)
        {
            this.db = db;
        }

        public async Task<Message> CreateAsync(string content, string user)
        {
            var message = new Message()
            {
                Content = content,
                User = user,
                CreatedOn = DateTime.UtcNow,
            };

            await this.db.Messages.AddAsync(message);
            await this.db.SaveChangesAsync();

            return message;
        }

        public async Task<IEnumerable<Message>> AllAsync()
        {
            var messages = await this.db
                .Messages
                .OrderBy(m => m.CreatedOn)
                .ToListAsync();

            return messages;
        }
    }
}
