using DataGrokrA2.BusinessServices;
using ServiceStack.FluentValidation.Attributes;

namespace DataGrokrA2.BDL
{
    [Validator(typeof(BooksValidator))]
    public class BooksBDL
    {
        public int BookId { get; set; }
        public string Isbn { get; set; }
        public string Abbrv { get; set; }
        public string BookTitle { get; set; }
        public short TotalPages { get; set; }
        public int? AuthorId { get; set; }
    }
}
