using ChildVac.WebApi.Domain.Entities;
using FluentValidation;

namespace ChildVac.WebApi.Domain.Validators
{
    public class HospitalValidator : AbstractValidator<Hospital>
    {
        public HospitalValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(0, 50);

            RuleFor(x => x.Address)
                .NotEmpty()
                .Length(0, 250);
        }
    }
}
