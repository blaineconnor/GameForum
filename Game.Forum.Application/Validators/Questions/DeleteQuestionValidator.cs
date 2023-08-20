using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Question;

namespace Game.Forum.Application.Validators.Questions
{
    public class DeleteQuestionValidator : AbstractValidator<DeleteQuestionVM>
    {
        public DeleteQuestionValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty()
                .WithMessage("Soru kimlik numarası boş bırakılamaz.")
                .GreaterThan(0)
                .WithMessage("Soru kimlik bilgisi sıfırdan büyük olmalıdır.");
        }
    }
}
