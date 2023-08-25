﻿using FluentValidation;
using Game.Forum.UI.Models.RequestModels.Categories;

namespace Game.Forum.UI.Validators.Categories
{
    public class GetCategoryByIdValidator : AbstractValidator<GetCategoryByIdVM>
    {
        public GetCategoryByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Kategori kimlik numarası boş bırakılamaz.")
                .GreaterThan(0)
                .WithMessage("Kategori kimlik bilgisi sıfırdan büyük olmalıdır.");
        }
    }
}
