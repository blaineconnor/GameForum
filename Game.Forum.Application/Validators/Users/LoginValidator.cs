using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Users;

namespace Game.Forum.Application.Validators.Users
{
    public class LoginValidator : AbstractValidator<LoginVM>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MaximumLength(10).WithMessage("Kullanıcı adı en fazla 10 karakter olabilir.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola boş olamaz.")
                .MaximumLength(10).WithMessage("Parola en fazla 10 karakter olabilir.");
        }
    }
}
