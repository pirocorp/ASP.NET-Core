namespace BookLibrary.Data.Models
{
    using System.Collections.Generic;

    public class Borrower
    {
        public Borrower()
        {
            this.Books = new HashSet<BookBorrower>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<BookBorrower> Books { get; set; }
    }
}
