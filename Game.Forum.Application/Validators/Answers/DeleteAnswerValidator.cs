using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Answers;

namespace Game.Forum.Application.Validators.Answers
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
