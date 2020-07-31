namespace MessagesAPI.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MessagesAPI.Data;
    using MessagesAPI.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class MessagesService : IMessagesService
    {
        private readonly MessagesDbContext db;

        public MessagesService(MessagesDbContext db)
        {
            this.db = db;
        }

        public async Task<Message> CreateAsync(string content, User user)
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

        public async Task<IEnumerable<MessageServiceListingModel>> AllAsync(int? count)
        {
            var query = this.db
                .Messages
                .OrderByDescending(m => m.CreatedOn)
                .Select(m => new MessageServiceListingModel()
                {
                    Content = m.Content,
                    User = m.User.Username,
                });

            if (count.HasValue)
            {
                query = query
                    .Take(count.Value);
            }

            var messages = await query
                .ToListAsync();

            messages.Reverse(0, messages.Count);

            return messages;
        }
    }
}
