namespace Panda.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Panda.Models;

    public class PandaUserConfiguration : IEntityTypeConfiguration<PandaUser>
    {
        public void Configure(EntityTypeBuilder<PandaUser> pandaUser)
        {
            pandaUser
                .HasMany(u => u.Receipts)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            pandaUser
                .HasMany(u => u.Packages)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
