using AutoMapper;
using Game.Forum.Application.Behaviors;
using Game.Forum.Application.Exceptions;
using Game.Forum.Application.Models.DTOs.Delete;
using Game.Forum.Application.Models.RequestModels.Answers;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Domain.Cache.Redis;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;

namespace Game.Forum.Application.Services.Implementation
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IRedisCache _redisCache;
        private readonly string GetAllQuestionsContractKey = "GetAllQuestionsContract";
        private readonly string QuestionDetailResponseContractKey = "QuestionDetailResponseContract";

        public AnswerService(IAnswerRepository answerRepository, IUserRepository userRepository, IQuestionRepository questionRepository, IMapper mapper, IRedisCache redisCache)
        {
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        [PerformanceBehavior]
        public async Task AddAnswerAsync(AddAnswerVM addAnswerVM)
        {

            var user = await _userRepository.GetByIdAsync(addAnswerVM.UserId);
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
            await _redisCache.Remove(GetAllQuestionsContractKey);
            await _redisCache.Remove(QuestionDetailResponseContractKey);
        }

        public async Task DeleteAnswerAsync(DeleteDto deleteDto)
        {
            var dbAnswer = await _answerRepository.GetByIdAsync(deleteDto.Id);
            if (dbAnswer?.IsDeleted == true) { throw new ClientSideException("Böyle bir Yanıt yok"); };
            await _answerRepository.RemoveAsync(dbAnswer);
        }
    }
}
