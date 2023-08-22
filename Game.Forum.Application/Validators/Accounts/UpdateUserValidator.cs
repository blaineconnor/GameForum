using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Accounts;

namespace Game.Forum.Application.Validators.Accounts
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserVM>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Ad bilgisi boş olamaz.")
               .MaximumLength(30).WithMessage("Ad bilgisi 30 karakterden büyük olamaz.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad bilgisi boş olamaz.")
                .MaximumLength(30).WithMessage("Soyad bilgisi 30 karakterden büyük olamaz.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("e-Posta bilgisi boş olamaz.")
                .MaximumLength(40).WithMessage("e-Posta bilgisi 40 karakterden büyük olamaz")
                .EmailAddress().WithMessage("Geçerli bir e-Posta adresi girmediniz");
            RuleFor(x => x.Birthdate)
                .NotEmpty().WithMessage("Doğum tarihi bilgisi boş olamaz.");
        }
    }
}
