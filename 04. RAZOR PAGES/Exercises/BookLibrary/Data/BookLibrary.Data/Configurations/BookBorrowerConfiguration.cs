namespace BookLibrary.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class BookBorrowerConfiguration 
        : IEntityTypeConfiguration<BookBorrower>
    {
        public void Configure(EntityTypeBuilder<BookBorrower> builder)
        {
            builder
                .HasKey(bb => bb.Id);

            builder
                .HasOne(bb => bb.Book)
                .WithMany(b => b.Borrowers)
                .HasForeignKey(bb => bb.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(bb => bb.Borrower)
                .WithMany(b => b.Books)
                .HasForeignKey(bb => bb.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
