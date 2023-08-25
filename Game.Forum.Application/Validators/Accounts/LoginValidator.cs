using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Accounts;

namespace Game.Forum.Application.Validators.Accounts
{
    public class LoginValidator : AbstractValidator<LoginVM>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MaximumLength(30).WithMessage("Kullanıcı adı en fazla 30 karakter olabilir.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola boş olamaz.")
                .MaximumLength(30).WithMessage("Parola en fazla 30 karakter olabilir.");
        }
    }
}
