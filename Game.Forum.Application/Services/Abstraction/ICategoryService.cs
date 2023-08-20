using Game.Forum.Application.Models.DTOs.Category;
using Game.Forum.Application.Models.RequestModels.Categories;
using Game.Forum.Application.Wrapper;

namespace Game.Forum.Application.Services.Abstraction
{
    public interface ICategoryService
    {
        #region Seç

        Task<Result<List<CategoryDto>>> GetAllCategories();
        Task<Result<CategoryDto>> GetCategoryById(GetCategoryByIdVM getCategoryByIdVM);

        #endregion

        #region Ekle, Güncelle, Sil

        Task<Result<int>> CreateCategory(CreateCategoryVM createCategoryVM);
        Task<Result<int>> UpdateCategory(UpdateCategoryVM updateCategoryVM);
        Task<Result<int>> DeleteCategory(DeleteCategoryVM deleteCategoryVM);

        #endregion
    }
}
