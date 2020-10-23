namespace Panda.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Panda.Models;

    public class PackageStatusesConfiguration : IEntityTypeConfiguration<PackageStatus>
    {
        public void Configure(EntityTypeBuilder<PackageStatus> packageStatus)
        {
            packageStatus
                .HasIndex(ps => ps.Name)
                .IsUnique();
        }
    }
}
