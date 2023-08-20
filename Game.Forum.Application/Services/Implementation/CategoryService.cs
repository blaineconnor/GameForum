using AutoMapper;
using AutoMapper.QueryableExtensions;
using Game.Forum.Application.Behaviors;
using Game.Forum.Application.Exceptions;
using Game.Forum.Application.Models.DTOs.Category;
using Game.Forum.Application.Models.RequestModels.Categories;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Application.Validators.Categories;
using Game.Forum.Application.Wrapper;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.UnitofWork;
using System.Data.Entity;

namespace Game.Forum.Application.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _db;

        public CategoryService(IMapper mapper, IUnitofWork db)
        {
            _mapper = mapper;
            _db = db;
        }

        #region Tüm kategorileri getir 

        [PerformanceBehavior]
        public async Task<Result<List<CategoryDto>>> GetAllCategories()
        {
            var result = new Result<List<CategoryDto>>();

            var categoryEntites = await _db.GetRepository<Category>().GetAllAsync();

            var categoryDtos = await categoryEntites.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            result.Data = categoryDtos;
            _db.Dispose();
            return result;
        }

        #endregion

        #region Tüm kategorileri ID'ye göre getir

        [ValidationBehavior(typeof(GetCategoryByIdValidator))]
        public async Task<Result<CategoryDto>> GetCategoryById(GetCategoryByIdVM getCategoryByIdVM)
        {
            var result = new Result<CategoryDto>();

            var categoryExists = await _db.GetRepository<Category>().AnyAsync(x => x.Id == getCategoryByIdVM.Id);
            if (!categoryExists)
            {
                throw new NotFoundException($"{getCategoryByIdVM.Id} numaralı kategori bulunamadı.");
            }

            var categoryEntity = await _db.GetRepository<Category>().GetById(getCategoryByIdVM.Id);

            var categoryDto = _mapper.Map<Category, CategoryDto>(categoryEntity);

            result.Data = categoryDto;
            _db.Dispose();
            return result;
        }

        #endregion

        #region Kategori oluştur

        [ValidationBehavior(typeof(CreateCategoryValidator))]
        public async Task<Result<int>> CreateCategory(CreateCategoryVM createCategoryVM)
        {
            var result = new Result<int>();

            var categoryEntity = _mapper.Map<CreateCategoryVM, Category>(createCategoryVM);

            _db.GetRepository<Category>().Add(categoryEntity);
            await _db.CommitAsync();

            result.Data = categoryEntity.Id;
            _db.Dispose();
            return result;
        }

        #endregion

        #region Kategori sil

        [ValidationBehavior(typeof(DeleteCategoryValidator))]
        public async Task<Result<int>> DeleteCategory(DeleteCategoryVM deleteCategoryVM)
        {
            var result = new Result<int>();

            var categoryExists = await _db.GetRepository<Category>().AnyAsync(x => x.Id == deleteCategoryVM.Id);
            if (!categoryExists)
            {
                throw new NotFoundException($"{deleteCategoryVM.Id} numaralı kategori bulunamadı.");
            }

            _db.GetRepository<Category>().Delete(deleteCategoryVM.Id);
            await _db.CommitAsync();

            result.Data = deleteCategoryVM.Id;
            _db.Dispose();
            return result;
        }

        #endregion

        #region Kategori güncelle 

        [ValidationBehavior(typeof(UpdateCategoryValidator))]
        public async Task<Result<int>> UpdateCategory(UpdateCategoryVM updateCategoryVM)
        {
            var result = new Result<int>();

            var existsCategory = await _db.GetRepository<Category>().GetById(updateCategoryVM.Id);
            if (existsCategory is null)
            {
                throw new Exception($"{updateCategoryVM} numaralı kategori bulunamadı.");
            }


            var updatedCategory = _mapper.Map(updateCategoryVM, existsCategory);

            _db.GetRepository<Category>().Update(updatedCategory);
            await _db.CommitAsync();

            result.Data = updatedCategory.Id;
            _db.Dispose();
            return result;
        }

        #endregion
    }
}
