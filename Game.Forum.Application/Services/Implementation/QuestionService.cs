using AutoMapper;
using Game.Forum.Application.Behaviors;
using Game.Forum.Application.Exceptions;
using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.DTOs.Question;
using Game.Forum.Application.Models.RequestModels.Question;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Application.Validators.Questions;
using Game.Forum.Domain.Cache.Abstraction;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;
using Game.Forum.Domain.UnitofWork;

namespace Game.Forum.Application.Services.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IUnitofWork _uWork;
        private readonly IFavoriteCache _favoriteCache;
        private readonly IQuestionDetailCache _questionDetailCache;
        private readonly IVoteCache _voteCache;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IFavoriteRepository favoriteRepository,
            IUnitofWork uWork, IFavoriteCache favoriteCache = null, IQuestionDetailCache questionDetailCache = null, IVoteCache voteCache = null)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _favoriteRepository = favoriteRepository;
            _uWork = uWork;
            _favoriteCache = favoriteCache;
            _questionDetailCache = questionDetailCache;
            _voteCache = voteCache;
        }
        #region Add Question

        [PerformanceBehavior]
        [ValidationBehavior(typeof(AddQuestionValidator))]

        public async Task AddQuestionAsync(AddQuestionVM addQuestionVM)
        {
            var addQuestion = _mapper.Map<Question>(addQuestionVM);
            await _questionRepository.AddAsync(addQuestion);
        }

        #endregion

        #region Update Question

        [ValidationBehavior(typeof(UpdateQuestionValidator))]

       public async Task UpdateQuestion(UpdateQuestionVM updateQuestionVM)
        {
            var updateQuestion = _mapper.Map<Question>(updateQuestionVM);
            await _questionRepository.UpdateAsync(updateQuestion);
        }

        #endregion

        #region Delete Question

        [ValidationBehavior(typeof(DeleteQuestionValidator))]

        public async Task DeleteQuestion(DeleteDto deleteDto)
        {
            var dbQuestion = await _questionRepository.GetByIdAsync(deleteDto.Id);
            if (dbQuestion == null)
            {
                throw new ClientSideException("Soru bulunamadı");
            }
            await _questionRepository.RemoveAsync(dbQuestion);
        }

        #endregion

        #region Favorites Tasks

        public async Task DeleteFavorite(DeleteDto deleteDto)
        {
            var dbFavorite = await _favoriteRepository.GetByIdAsync(deleteDto.Id);
            if (dbFavorite == null)
            {
                throw new ClientSideException("Favorite Bulunumadı");
            }
            await _favoriteRepository.RemoveAsync(dbFavorite);
        }

        
        private async Task AddQuestionToFavHelper(AddQuestionToFav addQuestionToFavVM)
        {
            var user = await _uWork.GetRepository<Account>().GetById(addQuestionToFavVM.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var question = await _questionRepository.GetByIdAsync(addQuestionToFavVM.QuestionId);
            if (question == null)
            {
                throw new NotFoundException("Question not found");
            }
        }

        public async Task AddQuestionToFavAsync(AddQuestionToFav addQuestionToFavVM)
        {
            await CheckIfUserFavorited(addQuestionToFavVM);
            await AddQuestionToFavHelper(addQuestionToFavVM);
            var model = _mapper.Map<Favorite>(addQuestionToFavVM);
            await _favoriteRepository.AddAsync(model);
            await _favoriteCache.RemoveFavoriteCache(addQuestionToFavVM.QuestionId, addQuestionToFavVM.UserId);
        }

        #endregion

        #region Checking Sections

        private async Task CheckIfUserFavorited(AddQuestionToFav addQuestionToFavDto)
        {
            var favorite = await _favoriteRepository.CheckFavorite(addQuestionToFavDto.QuestionId, addQuestionToFavDto.UserId);
            if (favorite)
            { throw new ClientSideException("bi kere daha favorilenemez"); }
        }
        private Task CheckIfPageExist(Pagination pagination, PaginationResponse<GetAllQuestions> questions)
        {
            if (pagination.Page > questions.Pagination.TotalPage)
            {
                throw new ClientSideException("sayfada veri yok");
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Get All Tasks

        [PerformanceBehavior]
        public async Task<PaginationResponse<GetAllQuestions>> GetNewestQuestions(Pagination pagination)
        {
            var questions = await _questionRepository.GetNewestQuestions(pagination);
            await CheckIfPageExist(pagination, questions);
            return questions;
        }

        [PerformanceBehavior]
        public async Task<PaginationResponse<GetAllQuestions>> GetQuestionsByDescendingVote(Pagination pagination)
        {
            var questions = await _questionRepository.GetQuestionsByDescendingAnswer(pagination);
            await CheckIfPageExist(pagination, questions);
            return questions;
        }

        [PerformanceBehavior]
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

        #endregion





      
    }
}
