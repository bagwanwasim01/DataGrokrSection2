using DataGrokrA2.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGrokrA2.BusinessServices
{
    public class AuthorsValidator :AbstractValidator<Author>
    {
        public AuthorsValidator()
        {
            RuleFor(x => x.AuthorId).GreaterThan(0).WithMessage("The Author ID must be greater than 0");
            RuleFor(x => x.Abbrv)
               .Length(0,10)
               .WithMessage("The Author's Abbrivation can not be more than 10 characters");
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("The Author Name can not be blank")
                .Length(0, 20)
                .WithMessage("The Author Name can not be more than 20 characters");
            RuleFor(x => x.LastName)             
              .Length(0, 20)
              .WithMessage("The Author LastName can not be more than 20 characters");
            RuleFor(x => x.BookId).GreaterThan(0).NotNull().WithMessage("The Book ID must be provided");

        }
    }
}
