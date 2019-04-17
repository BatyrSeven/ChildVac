using ChildVac.WebApi.Domain.Entities;
using FluentValidation;

namespace ChildVac.WebApi.Domain.Validators
{
    public class AdminValidator : AbstractValidator<Admin>
    {
        public AdminValidator()
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
        }
    }
}
