using AutoMapper;
using Game.Forum.Application.Models.RequestModels.Accounts;
using Game.Forum.Application.Models.RequestModels.Answers;
using Game.Forum.Application.Models.RequestModels.Categories;
using Game.Forum.Application.Models.RequestModels.Question;
using Game.Forum.Application.Models.RequestModels.Votes;
using Game.Forum.Domain.Entities;

namespace Game.Forum.Application.AutoMappings
{
    public class ViewModelToDomain : Profile
	{
        public ViewModelToDomain()
		{
            #region Membership

            CreateMap<RegisterVM, User>();
            CreateMap<RegisterVM, Account>()
                .ForMember(x => x.Role, y => y.MapFrom(e => Roles.User));

            CreateMap<UpdateUserVM, User>();
            CreateMap<UpdateUserVM, Account>();


            #endregion


            #region Forum

            //Kategori
            CreateMap<CreateCategoryVM, Category>()
                .ForMember(x => x.CategoryName, y => y.MapFrom(e => e.CategoryName));

            CreateMap<UpdateCategoryVM, Category>()
                .ForMember(x => x.CategoryName, y => y.MapFrom(e => e.CategoryName));

            //Yorum
            CreateMap<AddQuestionVM, Question>();

            CreateMap<AddQuestionToFavVM, Favorite>();
            CreateMap<AddAnswerVM, Answer>();

            //Oy
            CreateMap<AddVoteVM, Vote>();


            #endregion                                

        }
	}
}
