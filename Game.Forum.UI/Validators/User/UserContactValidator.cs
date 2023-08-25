using FluentValidation;
using Game.Forum.UI.Models.RequestModels.User;

namespace Game.Forum.UI.Validators.User
{
    public class UserContactValidator : AbstractValidator<UserContactVM>
    {
        public UserContactValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotEmpty().WithMessage("Kullanıcı adı bilgisi boş olamaz.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull().WithMessage("e-Posta bilgisi boş olamaz.");
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull().WithMessage("Konu bilgisi boş olamaz");
            RuleFor(x => x.Details)
                .NotNull()
                .NotEmpty()
                .MaximumLength(1000).WithMessage("Detaylar bölümü boş olamaz ve en fazla 1000 karakter olmalıdır.");

        }
    }
}
