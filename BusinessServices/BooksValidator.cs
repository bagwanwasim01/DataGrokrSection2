using DataGrokrA2.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGrokrA2.BusinessServices
{
    public class BooksValidator : AbstractValidator<Book>
    {
        public BooksValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0).WithMessage("The Book ID must be greater than 0");
            RuleFor(x => x.Abbrv)
               .Length(0, 10)
               .WithMessage("The Book's Abbrivation can not be more than 10 characters");
            RuleFor(x => x.BookTitle)
                .NotEmpty()
                .WithMessage("The Book Title can not be blank")
                .Length(0, 20)
                .WithMessage("The Book Title can not be more than 20 characters");                                   
            RuleFor(x => x.AuthorId).GreaterThan(0).NotNull().WithMessage("The Author ID must be provided");
            RuleFor(x => x.Isbn).Length(13).WithMessage("The ISBN No. must be 13 integer long");

        }
    }
}
