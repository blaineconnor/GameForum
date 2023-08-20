using AutoMapper;
using Game.Forum.Application.Exceptions;
using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.DTOs.Question;
using Game.Forum.Application.Models.RequestModels.Question;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Domain.Cache.Abstraction;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;

namespace Game.Forum.Application.Services.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFavoriteCache _favoriteCache;
        private readonly IQuestionDetailCache _questionDetailCache;
        private readonly IVoteCache _voteCache;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IFavoriteRepository favoriteRepository,
            IUserRepository userRepository, IFavoriteCache favoriteCache = null, IQuestionDetailCache questionDetailCache = null, IVoteCache voteCache = null)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _favoriteRepository = favoriteRepository;
            _userRepository = userRepository;
            _favoriteCache = favoriteCache;
            _questionDetailCache = questionDetailCache;
            _voteCache = voteCache;
        }

        public async Task AddQuestionAsync(AddQuestionVM addQuestionVM)
        {
            var addQuestion = _mapper.Map<Question>(addQuestionVM);
            await _questionRepository.AddAsync(addQuestion);
        }

       
        public async Task DeleteFavorite(DeleteDto deleteDto)
        {
            var dbFavorite = await _favoriteRepository.GetByIdAsync(deleteDto.Id);
            if (dbFavorite == null)
            {
                throw new ClientSideException("Favorite Bulunumadı");
            }
            await _favoriteRepository.RemoveAsync(dbFavorite);
        }

        public async Task DeleteQuestion(DeleteDto deleteDto)
        {
            var dbQuestion = await _questionRepository.GetByIdAsync(deleteDto.Id);
            if (dbQuestion == null)
            {
                throw new ClientSideException("Soru bulunamadı");
            }
            await _questionRepository.RemoveAsync(dbQuestion);
        }

        

        private async Task AddQuestionToFavHelper(AddQuestionToFavDto addQuestionToFavDto)
        {
            var user = await _userRepository.GetByIdAsync(addQuestionToFavDto.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var question = await _questionRepository.GetByIdAsync(addQuestionToFavDto.QuestionId);
            if (question == null)
            {
                throw new NotFoundException("Question not found");
            }
        }
        private async Task CheckIfUserFavorited(AddQuestionToFavDto addQuestionToFavDto)
        {
            var favorite = await _favoriteRepository.CheckFavorite(addQuestionToFavDto.QuestionId, addQuestionToFavDto.UserId);
            if (favorite)
            { throw new ClientSideException("bi kere daha favorilenemez"); }
        }
        private async Task CheckIfPageExist(Pagination pagination, PaginationResponse<GetAllQuestions> questions)
        {
            if (pagination.Page > questions.Pagination.TotalPage)
            {
                throw new ClientSideException("sayfada veri yok");
            }
        }

        public async Task<PaginationResponse<GetAllQuestions>> GetNewestQuestions(Pagination pagination)
        {
            var questions = await _questionRepository.GetNewestQuestions(pagination);
            await CheckIfPageExist(pagination, questions);
            return questions;
        }

        public async Task<PaginationResponse<GetAllQuestions>> GetQuestionsByDescendingVote(Pagination pagination)
        {
            var questions = await _questionRepository.GetQuestionsByDescendingAnswer(pagination);
            await CheckIfPageExist(pagination, questions);
            return questions;
        }

        public async Task<PaginationResponse<GetAllQuestions>> GetQuestionsByDescendingAnswer(Pagination pagination)
        {
            var questions = await _questionRepository.GetQuestionsByDescendingAnswer(pagination);
            await CheckIfPageExist(pagination, questions);
            return questions;
        }

        public async Task<QuestionDetailResponse> GetQuestionsWithDetail(int id, int userId)
        {
            var isFavorite = await _favoriteCache.CheckFav(id, userId);
            var questionResponse = await _questionDetailCache.GetQuestionsWithDetail(id);
            questionResponse.Vote = await _voteCache.GetNumberOfVotes(id);
            questionResponse.IsFavorite = isFavorite;
            return questionResponse;
        }

        public async Task AddQuestionToFavAsync(AddQuestionToFavVM addQuestionToFavVM)
        {
            var favorite = await _favoriteRepository.CheckFavorite(addQuestionToFavVM.QuestionId, addQuestionToFavVM.UserId);
            if (favorite)
            { throw new ClientSideException("bi kere daha favorilenemez"); }
        }
    }
}
