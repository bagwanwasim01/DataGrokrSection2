using System;
using System.Collections.Generic;

namespace DataGrokrA2.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int AuthorId { get; set; }
        public string Abbrv { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? Dod { get; set; }
        public int? BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
