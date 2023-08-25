using AutoMapper;
using Game.Forum.UI.Models.DTOs.Category;
using Game.Forum.UI.Models.RequestModels.Categories;

namespace Game.Forum.UI.ModelMappings
{
    public class DtoToVm : Profile
    {
        public DtoToVm()
        {
            CreateMap<CategoryDto, UpdateCategoryVM>()
                .ForMember(x => x.CategoryName, y => y.MapFrom(e => e.CategoryName));
        }
    }
}
