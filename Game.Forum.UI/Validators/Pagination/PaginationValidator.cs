using FluentValidation;
using Game.Forum.UI.Models.RequestModels.Pagination;

namespace Game.Forum.UI.Validators.Pagination
{
    public class PaginationValidator : AbstractValidator<PaginationVM>
    {
        public PaginationValidator()
        {
            RuleFor(p => p.Page)
                .NotEmpty()
                .NotNull();
            RuleFor(p => p.PageSize)
                .NotEmpty()
                .NotNull()
                .LessThanOrEqualTo(100);

        }
    }
}
