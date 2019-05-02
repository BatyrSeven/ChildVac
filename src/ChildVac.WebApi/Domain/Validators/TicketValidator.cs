using ChildVac.WebApi.Domain.Entities;
using FluentValidation;
using System;

namespace ChildVac.WebApi.Domain.Validators
{
    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator()
        {
            RuleFor(x => x.StartDateTime)
                .GreaterThan(DateTime.Now)
                .WithMessage("Время приема должно быть позже настоящего времени.");
        }
    }
}
