namespace Stopify.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> type)
        {
            type
                .HasIndex(t => t.Name)
                .IsUnique();

            type
                .Property(t => t.Name)
                .IsRequired();
        }
    }
}
