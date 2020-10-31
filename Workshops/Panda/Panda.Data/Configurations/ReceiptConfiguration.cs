namespace Panda.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Panda.Models;

    public class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> receipt)
        {
            receipt
                .HasOne(r => r.Package)
                .WithOne(p => p.Receipt)
                .HasForeignKey<Receipt>(r => r.PackageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
