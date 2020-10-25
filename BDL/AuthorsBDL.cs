using DataGrokrA2.BusinessServices;
using ServiceStack.FluentValidation.Attributes;
using System;

namespace DataGrokrA2.BDL
{
    [Validator(typeof(AuthorsValidator))]
    public class AuthorsBDL
    {
        public int AuthorId { get; set; }
        public string Abbrv { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? Dod { get; set; }
        public int? BookId { get; set; }
    }
}
