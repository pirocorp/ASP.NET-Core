namespace Eventures.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class EventuresDbContext : IdentityDbContext<EventuresUser, IdentityRole, string>
    {
        public EventuresDbContext(DbContextOptions<EventuresDbContext> options)
            : base(options)
        { }

        public DbSet<Event> Events { get; set; }

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
