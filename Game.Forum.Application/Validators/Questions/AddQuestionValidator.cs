using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Question;

namespace Game.Forum.Application.Validators.Questions
{
    public class AddQuestionValidator : AbstractValidator<AddQuestionVM>
    {
        public AddQuestionValidator()
        {
            RuleFor(u => u.UserId)
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
