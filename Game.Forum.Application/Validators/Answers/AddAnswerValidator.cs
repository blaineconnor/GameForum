using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Answers;

namespace Game.Forum.Application.Validators.Answers
{
    public class AddAnswerValidator : AbstractValidator<AddAnswerVM>
    {
        public AddAnswerValidator()
        {
            RuleFor(u => u.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Kullanıcı kimliği boş olamaz.")
                .GreaterThan(0)
                .WithMessage("Kullanıcı kimlik bilgisi sıfırdan büyük olmalıdır.");


            RuleFor(u => u.QuestionId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Soru kimliği boş olamaz.")
                .GreaterThan(0)
                .WithMessage("Soru kimlik bilgisi sıfırdan büyük olmalıdır.");


            RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty()
                .WithMessage("İçerik boş olamaz.")
                .MaximumLength(1000)
                .WithMessage("İçerik 1000 karakterden fazla olamaz.");
        }
    }
}
