namespace MessagesAPI.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class MessagesDbContext : DbContext
    {
        public MessagesDbContext(DbContextOptions<MessagesDbContext> options)
            : base(options)
        { }

        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureRelations(builder);
        }

        // Applies configurations
        private void ConfigureRelations(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
