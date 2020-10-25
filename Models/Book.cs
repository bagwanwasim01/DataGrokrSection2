using System;
using System.Collections.Generic;

namespace DataGrokrA2.Models
{
    public partial class Book
    {
        public Book()
        {
            Authors = new HashSet<Author>();
        }

        public int BookId { get; set; }
        public string Isbn { get; set; }
        public string Abbrv { get; set; }
        public string BookTitle { get; set; }
        public string EditionNumber { get; set; }
        public short TotalPages { get; set; }
        public int? AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}
