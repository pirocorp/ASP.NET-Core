namespace CameraBazaar.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CameraBazaarDbContext : IdentityDbContext
    {
        public CameraBazaarDbContext(DbContextOptions<CameraBazaarDbContext> options)
            : base(options)
        { }

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
