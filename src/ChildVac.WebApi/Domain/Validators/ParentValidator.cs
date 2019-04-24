using ChildVac.WebApi.Domain.Entities;
using FluentValidation;

namespace ChildVac.WebApi.Domain.Validators
{
    public class ParentValidator : AbstractValidator<Parent>
    {
        public ParentValidator()
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

            RuleFor(x => x.Gender)
                .NotNull()
                .NotEqual(Gender.Undefined);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Length(10);

            RuleFor(x => x.Address)
                .NotEmpty()
                .Length(0, 100);
        }
    }
}
