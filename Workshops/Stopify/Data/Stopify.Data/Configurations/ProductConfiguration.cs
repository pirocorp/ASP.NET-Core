namespace Stopify.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Stopify.Data.Models;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> product)
        {
            product
                .HasOne(p => p.Type)
                .WithMany(t => t.Products)
                .HasForeignKey(p => p.TypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
