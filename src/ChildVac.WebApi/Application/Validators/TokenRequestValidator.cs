using ChildVac.WebApi.Application.Models;
using FluentValidation;

namespace ChildVac.WebApi.Application.Validators
{
    public class TokenRequestValidator : AbstractValidator<TokenRequestModel>
    {
        public TokenRequestValidator()
        {
            RuleFor(x => x.Iin)
                .NotEmpty()
                .Length(12);

            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x.Role)
                .NotEmpty();
        }
    }
}
