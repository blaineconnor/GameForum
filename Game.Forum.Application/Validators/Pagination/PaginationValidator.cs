using FluentValidation;
using Game.Forum.Application.Models.RequestModels.Pagination;

namespace Game.Forum.Application.Validators.Pagination
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
