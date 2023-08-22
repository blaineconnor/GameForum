using AutoMapper;
using Game.Forum.Application.Models.DTOs.Accounts;
using Game.Forum.Application.Models.DTOs.Answer;
using Game.Forum.Application.Models.DTOs.Category;
using Game.Forum.Application.Models.DTOs.Question;
using Game.Forum.Application.Models.DTOs.User;
using Game.Forum.Application.Models.DTOs.Vote;
using Game.Forum.Domain.Entities;

namespace Game.Forum.Application.AutoMappings
{
    public class DomainToDto : Profile
    {
        public DomainToDto()
        {
            //Kullanıcı
            CreateMap<User, UserDto>();
            CreateMap<Account, AccountDto>();


            //Kategori
            CreateMap<Category, CategoryDto>();


            //Cevap
            CreateMap<Answer, AddAnswerDto>()
                .ForMember(x => x.QuestionId, y => y.MapFrom(e => e.QuestionId))
                .ForMember(x => x.UserId, y => y.MapFrom(e => e.User))
                .ForMember(x => x.Content, y => y.MapFrom(e => e.Content));
            
            
            //Oy
            CreateMap<Vote, AddVoteDto>()
                .ForMember(x => x.QuestionId, y => y.MapFrom(e => e.QuestionId))
                .ForMember(x => x.UserId, y => y.MapFrom(e => e.User))
                .ForMember(x => x.Voted, y => y.MapFrom(e => e.Voted));


            //Soru
            CreateMap<Question, GetAllQuestionsDto>()
                .ForMember(x => x.User, y => y.MapFrom(e => e.User))
                .ForMember(x => x.Title, y => y.MapFrom(e => e.Title))
                .ForMember(x => x.Content, y => y.MapFrom(e => e.Content))
                .ForMember(x => x.CreatedDate, y => y.MapFrom(e => e.CreatedDate))
                .ForMember(x => x.Category, y => y.MapFrom(e => e.Categories))
                .ForMember(x => x.Vote, y => y.MapFrom(e => e.Votes))
                .ForMember(x => x.View, y => y.MapFrom(e => e.QuestionViews))
                .ForMember(x => x.Answer, y => y.MapFrom(e => e.Answers));
                

        }
    }
}
