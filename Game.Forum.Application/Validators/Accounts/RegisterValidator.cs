using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Accounts;

namespace Game.Forum.Application.Validators.Accounts
{
    public class RegisterValidator : AbstractValidator<RegisterVM>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad bilgisi boş olamaz.")
                .MaximumLength(30).WithMessage("Ad bilgisi 30 karakterden büyük olamaz.");

            RuleFor(x => x.Surname)
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
                .Equal(x => x.EmailAgain)
                .When(x => !String.IsNullOrWhiteSpace(x.Email))
                .WithMessage("e-Posta ve e-Posta tekrar bilgisi eşleşmiyor.");


            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Doğum tarihi bilgisi boş olamaz.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Cinsiyet bilgisi boş olamaz.")
                .IsInEnum().WithMessage("Cinsiyet bilgisi geçerli değil. (1 veya 2 olabilir.)");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MaximumLength(30).WithMessage("Kullanıcı adı en fazla 30 karakter olabilir.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola boş olamaz.")
                .MaximumLength(30).WithMessage("Parola en fazla 30 karakter olabilir.");

            RuleFor(x => x.PasswordAgain)
                .NotEmpty().WithMessage("Parola tekrar bilgisi boş olamaz.")
                .MaximumLength(30).WithMessage("Parola tekrar bilgisi 30 karakter olabilir.");

            RuleFor(x => x.Password)
                .Equal(x => x.PasswordAgain)
                .When(x => !String.IsNullOrWhiteSpace(x.Password))
                .WithMessage("Parola ve parola tekrar bilgisi eşleşmiyor.");
        }
    }
}
