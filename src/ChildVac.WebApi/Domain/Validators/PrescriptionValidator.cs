using ChildVac.WebApi.Domain.Entities;
using FluentValidation;

namespace ChildVac.WebApi.Domain.Validators
{
    public class PrescriptionValidator : AbstractValidator<Prescription>
    {
        public PrescriptionValidator()
        {
            RuleFor(x => x.Diagnosis)
                .NotEmpty();
        }
    }
}
