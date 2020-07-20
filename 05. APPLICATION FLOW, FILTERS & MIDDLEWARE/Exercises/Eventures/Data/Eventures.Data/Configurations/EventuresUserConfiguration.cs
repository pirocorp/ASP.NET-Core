namespace Eventures.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class EventuresUserConfiguration : IEntityTypeConfiguration<EventuresUser>
    {
        public void Configure(EntityTypeBuilder<EventuresUser> builder)
        {
            builder
                .HasIndex(u => u.Ucn)
                .IsUnique();
        }
    }
}
