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
                .Length(12)
                .WithMessage("ИИН состоит из 12 символов.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Пароль не должен быть пустым.");

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("Необходимо указать роль пользователя.");
        }
    }
}
