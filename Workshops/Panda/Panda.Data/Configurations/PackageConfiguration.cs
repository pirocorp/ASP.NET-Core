namespace Panda.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Panda.Models;

    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> package)
        {
            package
                .HasOne(p => p.Status)
                .WithMany(s => s.Packages)
                .HasForeignKey(p => p.StatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
