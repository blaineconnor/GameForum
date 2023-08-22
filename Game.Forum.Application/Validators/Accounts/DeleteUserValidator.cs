using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Accounts;

namespace Game.Forum.Application.Validators.Accounts
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserVM>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("Kullanıcı kimlik numarası boş bırakılamaz.")
                .GreaterThan(0)
                .WithMessage("Kullanıcı kimlik bilgisi sıfırdan büyük olmalıdır.");
        }
    }
}
