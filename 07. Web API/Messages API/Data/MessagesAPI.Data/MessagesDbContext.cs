namespace MessagesAPI.Data
{
    using Microsoft.EntityFrameworkCore;

    public class MessagesDbContext : DbContext
    {
        public MessagesDbContext(DbContextOptions<MessagesDbContext> options)
            : base(options)
        { }
    }
}
