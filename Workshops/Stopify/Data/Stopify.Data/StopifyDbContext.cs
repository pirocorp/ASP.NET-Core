namespace Stopify.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Stopify.Data.Models;

    public class StopifyDbContext : IdentityDbContext<StopifyUser, IdentityRole, string>
    {
        public StopifyDbContext(DbContextOptions<StopifyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);
        }

        /// <summary>
        ///     Applies all entity configurations, those who implements IEntityTypeConfiguration.
        /// </summary>
        /// <param name="builder">ModelBuilder.</param>
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
