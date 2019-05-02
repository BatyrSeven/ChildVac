using ChildVac.WebApi.Application.Models;
using FluentValidation;

namespace ChildVac.WebApi.Application.Validators
{
    /// <inheritdoc />
    /// <summary>
    /// Валидация модели для получения JWT токена авторизации
    /// </summary>
    public class TokenRequestValidator : AbstractValidator<TokenRequestModel>
    {
        /// <summary>
        /// Валидатор модели для получения JWT токена авторизации
        /// </summary>
        public TokenRequestValidator()
        {
            RuleFor(x => x.Iin)
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
