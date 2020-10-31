namespace Stopify.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> orderStatus)
        {
            orderStatus
                .HasIndex(os => os.Name)
                .IsUnique(true);
        }
    }
}
