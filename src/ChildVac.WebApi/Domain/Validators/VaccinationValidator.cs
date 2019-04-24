using ChildVac.WebApi.Domain.Entities;
using FluentValidation;

namespace ChildVac.WebApi.Domain.Validators
{
    public class VaccinationValidator : AbstractValidator<Vaccination>
    {
        public VaccinationValidator()
        {
            RuleFor(x => x.Vaccine)
                .NotNull();

            RuleFor(x => x.Ticket)
                .NotNull();
        }
    }
}
