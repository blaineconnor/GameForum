using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Answers;

namespace Game.Forum.Application.Validators.Answers
{
    public class UpdateAnswerValidator : AbstractValidator<UpdateAnswerVM>
    {
        public UpdateAnswerValidator()
        {
            RuleFor(x => x.AnswerId)
                .NotEmpty()
                .WithMessage("Cevap kimlik numarası boş bırakılamaz.")
                .GreaterThan(0)
                .WithMessage("Cevap kimlik bilgisi sıfırdan büyük olmalıdır.");
            RuleFor(x => x.TopicId)
                .NotEmpty()
                .WithMessage("Konu kimlik numarası boş bırakılamaz.")
                .GreaterThan(0)
                .WithMessage("Konu kimlik bilgisi sıfırdan büyük olmalıdır.");
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("İçerik boş olamaz.")
                .MaximumLength(1000)
                .WithMessage("İçeriği 1000 karakterden fazla olamaz.");
        }
    }
}
