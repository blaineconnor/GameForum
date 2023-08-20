using AutoMapper;
using Game.Forum.Application.Models.RequestModels.Categories;
using Game.Forum.Application.Models.RequestModels.Question;
using Game.Forum.Application.Models.RequestModels.Users;
using Game.Forum.Application.Models.RequestModels.Votes;
using Game.Forum.Domain.Entities;

namespace Game.Forum.Application.AutoMappings
{
    public class ViewModelToDomain : Profile
	{
        public ViewModelToDomain()
		{
            #region Membership

            CreateMap<RegisterVM, User>().ReverseMap();

            #endregion


            #region Forum

            //Kategori
            CreateMap<CreateCategoryVM, Category>()
                .ForMember(x => x.CategoryName, y => y.MapFrom(e => e.CategoryName)).ReverseMap();

            CreateMap<UpdateCategoryVM, Category>()
                .ForMember(x => x.CategoryName, y => y.MapFrom(e => e.CategoryName)).ReverseMap();

            //Yorum
            CreateMap<AddQuestionVM, Question>().ReverseMap();

            CreateMap<AddQuestionToFavVM, Favorite>().ReverseMap();

            //Oy
            CreateMap<AddVoteVM, Vote>().ReverseMap();

            #endregion                                

        }
	}
}
