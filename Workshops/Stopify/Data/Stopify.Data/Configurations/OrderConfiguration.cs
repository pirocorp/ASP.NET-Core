namespace Stopify.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> order)
        {
            order
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            order
                .HasMany(o => o.Products)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            order
                .HasOne(o => o.Status)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
