using AutoMapper;
using Game.Forum.Application.Behaviors;
using Game.Forum.Application.Exceptions;
using Game.Forum.Application.Models.RequestModels.Answers;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Application.Validators.Answers;
using Game.Forum.Domain.Cache.Redis;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;
using Game.Forum.Domain.UnitofWork;

namespace Game.Forum.Application.Services.Implementation
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUnitofWork _uWork;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IRedisCache _redisCache;
        private readonly string GetAllQuestionsVMKey = "GetAllQuestionsVM";
        private readonly string QuestionDetailResponseVMKey = "QuestionDetailResponseVM";

        public AnswerService(IAnswerRepository answerRepository, IUnitofWork uWork, IQuestionRepository questionRepository, IMapper mapper, IRedisCache redisCache)
        {
            _answerRepository = answerRepository;
            _uWork = uWork;
            _questionRepository = questionRepository;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        #region Add Answer

        [PerformanceBehavior]
        [ValidationBehavior(typeof(AddAnswerValidator))]
        public async Task AddAnswerAsync(AddAnswerVM addAnswerVM)
        {

            var user = await _uWork.GetRepository<Account>().GetById(addAnswerVM.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var question = await _questionRepository.GetByIdAsync(addAnswerVM.QuestionId);
            if (question == null)
            {
                throw new NotFoundException("User not found");
            }

            var addAnswer = _mapper.Map<Answer>(addAnswerVM);
            await _answerRepository.AddAsync(addAnswer);
            await _redisCache.Remove(GetAllQuestionsVMKey);
            await _redisCache.Remove(QuestionDetailResponseVMKey);
        }
        #endregion


        #region Delete Answer

        [ValidationBehavior(typeof(DeleteAnswerValidator))]
        public async Task DeleteAnswerAsync(DeleteAnswerVM deleteAnswerVM)
        {
            var dbAnswer = await _answerRepository.GetByIdAsync(deleteAnswerVM.AnswerId);
            if (dbAnswer?.IsDeleted == true) { throw new ClientSideException("Böyle bir Yanıt yok"); };
            await _answerRepository.RemoveAsync(dbAnswer);
        }

        #endregion


        #region Update Answer

        [ValidationBehavior(typeof(UpdateAnswerValidator))]
        public async Task UpdateAnswerAsync(UpdateAnswerVM updateAnswerVM)
        {
            var dbAnswer = await _answerRepository.GetByIdAsync(updateAnswerVM.AnswerId);
            if (dbAnswer?.IsDeleted == true) { throw new ClientSideException("Yanıt güncellendi"); };
            await _answerRepository.UpdateAsync(dbAnswer);
        }

        #endregion
    }
}
