using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Votes;

namespace Game.Forum.Application.Validators.Votes
{
    public class CreateVoteValidator : AbstractValidator<AddVoteVM>
    {
        public CreateVoteValidator()
        {
            RuleFor(x => x.Value)
                .GreaterThan(-1)
                .LessThan(1);

            RuleFor(x => x.VoteId)
                .NotEmpty()
                .WithMessage("Yorum kimlik numarası boş bırakılamaz.")
                .GreaterThan(0)
                .WithMessage("Yorum kimlik bilgisi sıfırdan büyük olmalıdır.");
        }
    }
}
