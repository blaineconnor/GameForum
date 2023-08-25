using FluentValidation;
using Game.Forum.UI.Models.RequestModels.Answers;

namespace Game.Forum.UI.Validators.Answers
{
    public class DeleteAnswerValidator : AbstractValidator<DeleteAnswerVM>
    {
        public DeleteAnswerValidator()
        {
            RuleFor(x => x.AnswerId)
                .NotEmpty()
                .WithMessage("Soru kimlik numarası boş bırakılamaz.")
                .GreaterThan(0)
                .WithMessage("Soru kimlik bilgisi sıfırdan büyük olmalıdır.");
        }
    }
}
