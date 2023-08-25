using FluentValidation;
using Game.Forum.UI.Models.RequestModels.Question;

namespace Game.Forum.UI.Validators.Questions
{
    public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionVM>
    {
        public UpdateQuestionValidator()
        {
            RuleFor(u => u.QuestionId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Kullanıcı kimlik numarası boş olamaz.");

            RuleFor(u => u.Category)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20)
                .WithMessage("Kategori bilgisi boş bırakılamaz.");
            RuleFor(u => u.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Başlık boş olamaz.");

            RuleFor(u => u.Content)
                .NotNull()
                .NotEmpty().WithMessage("İçerik boş olamaz.");
        }
    }
}
