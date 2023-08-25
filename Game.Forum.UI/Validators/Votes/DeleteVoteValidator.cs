using FluentValidation;
using Game.Forum.UI.Models.RequestModels.Votes;

namespace Game.Forum.UI.Validators.Votes
{
    public class DeleteVoteValidator : AbstractValidator<DeleteVoteVM>
    {
        public DeleteVoteValidator()
        {
            RuleFor(x => x.VoteId)
                .NotEmpty()
                .WithMessage("Yorum kimlik numarası boş bırakılamaz.")
                .GreaterThan(0)
                .WithMessage("Yorum kimlik bilgisi sıfırdan büyük olmalıdır.");
        }
    }
}
