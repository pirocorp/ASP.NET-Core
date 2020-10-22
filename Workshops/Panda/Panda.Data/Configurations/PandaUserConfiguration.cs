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
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            pandaUser
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            pandaUser
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            pandaUser
                .HasMany(u => u.Receipts)
                .WithOne(r => r.Recipient)
                .HasForeignKey(r => r.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            pandaUser
                .HasMany(u => u.Packages)
                .WithOne(p => p.Recipient)
                .HasForeignKey(p => p.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
