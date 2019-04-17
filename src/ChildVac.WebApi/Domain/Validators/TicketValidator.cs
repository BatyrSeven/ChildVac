using ChildVac.WebApi.Domain.Entities;
using FluentValidation;

namespace ChildVac.WebApi.Domain.Validators
{
    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator()
        {
            RuleFor(x => x.Child)
                .NotNull();

            RuleFor(x => x.Doctor)
                .NotNull();
        }
    }
}
