using System;
using ChildVac.WebApi.Domain.Entities;
using FluentValidation;

namespace ChildVac.WebApi.Domain.Validators
{
    public class ChildValidator : AbstractValidator<Child>
    {
        public ChildValidator()
        {
            RuleFor(x => x.Iin)
                .NotEmpty()
                .Length(12);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(0, 50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(0, 50);

            RuleFor(x => x.Patronim)
                .Length(0, 50);

            RuleFor(x => x.DateOfBirth)
                .NotNull()
                .LessThan(DateTime.Now.AddDays(1))
                .GreaterThan(DateTime.MinValue);

            RuleFor(x => x.Gender)
                .NotNull()
                .NotEqual(Gender.Undefined);

            RuleFor(x => x.ParentId)
                .NotNull()
                .NotEqual(0);
        }
    }
}
