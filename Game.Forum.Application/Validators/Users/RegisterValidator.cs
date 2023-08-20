using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Users;

namespace Game.Forum.Application.Validators.Users
{
    public class RegisterValidator : AbstractValidator<RegisterVM>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad bilgisi boş olamaz.")
                .MaximumLength(30).WithMessage("Ad bilgisi 30 karakterden büyük olamaz.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad bilgisi boş olamaz.")
                .MaximumLength(30).WithMessage("Soyad bilgisi 30 karakterden büyük olamaz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("e-Posta bilgisi boş olamaz.")
                .MaximumLength(40).WithMessage("e-Posta bilgisi 40 karakterden büyük olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-Posta adresi girmediniz.");

            RuleFor(x => x.EmailAgain)
                .NotEmpty().WithMessage("e-Posta bilgisi boş olamaz.")
                .MaximumLength(40).WithMessage("e-Posta bilgisi 40 karakterden büyük olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-Posta adresi girmediniz.");

            RuleFor(x => x.Email)
                .Matches(x => x.EmailAgain).WithMessage("e-Posta ve e-Posta Tekrar bilgisi eşleşmiyor. ");

            RuleFor(x => x.BirtDate)
                .NotEmpty().WithMessage("Doğum tarihi bilgisi boş olamaz.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Cinsiyet bilgisi boş olamaz.")
                .IsInEnum().WithMessage("Cinsiyet bilgisi geçerli değil. (1 veya 2 olabilir.)");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MaximumLength(10).WithMessage("Kullanıcı adı en fazla 10 karakter olabilir.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola boş olamaz.")
                .MaximumLength(10).WithMessage("Parola en fazla 10 karakter olabilir.");

            RuleFor(x => x.PasswordAgain)
                .NotEmpty().WithMessage("Parola tekrar bilgisi boş olamaz.")
                .MaximumLength(10).WithMessage("Parola tekrar bilgisi 10 karakter olabilir.");

            RuleFor(x => x.Password)
                .Matches(x => x.PasswordAgain).WithMessage("Parola ve parola tekrar bilgisi eşleşmiyor.");
        }
    }
}
