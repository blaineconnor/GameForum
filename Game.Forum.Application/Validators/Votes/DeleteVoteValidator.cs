using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Votes;

namespace Game.Forum.Application.Validators.Votes
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
