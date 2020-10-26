namespace ForumSystem.Data
{
    using System.Linq;

    using ForumSystem.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Makes column IsDeleted index on all entities implementing IDeletableEntity.
    /// </summary>
    internal static class EntityIndexesConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            // IDeletableEntity.IsDeleted index
            var deletableEntityTypes = modelBuilder.Model
                .GetEntityTypes()
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));

            foreach (var deletableEntityType in deletableEntityTypes)
            {
                modelBuilder.Entity(deletableEntityType.ClrType).HasIndex(nameof(IDeletableEntity.IsDeleted));
            }
        }
    }
}
