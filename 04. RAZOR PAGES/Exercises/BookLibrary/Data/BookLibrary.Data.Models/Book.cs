﻿namespace BookLibrary.Data.Models
{
    using System.Collections.Generic;

    public class Book
    {
        public Book()
        {
            this.Borrowers = new HashSet<BookBorrower>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CoverImage { get; set; }

        public BookStatus Status { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<BookBorrower> Borrowers { get; set; }
    }
}