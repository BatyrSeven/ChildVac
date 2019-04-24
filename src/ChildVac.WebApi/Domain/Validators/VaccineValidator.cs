using ChildVac.WebApi.Domain.Entities;
using FluentValidation;

namespace ChildVac.WebApi.Domain.Validators
{
    public class VaccineValidator : AbstractValidator<Vaccine>
    {
        public VaccineValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.RecieveMonth)
                .NotNull();

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}
