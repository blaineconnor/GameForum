using AutoMapper;
using Game.Forum.Application.Behaviors;
using Game.Forum.Application.Exceptions;
using Game.Forum.Application.Models.DTOs.Vote;
using Game.Forum.Application.Models.RequestModels.Votes;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Application.Validators.Votes;
using Game.Forum.Domain.Cache.Abstraction;
using Game.Forum.Domain.Cache.Keys;
using Game.Forum.Domain.Cache.Redis;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;
using Game.Forum.Domain.UnitofWork;

namespace Game.Forum.Application.Services.Implementation
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IUnitofWork _uWork;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IRedisCache _redisCache;
        private readonly IVoteCache _voteCache;

        public VoteService(IVoteRepository voteRepository, IUnitofWork uWork, IQuestionRepository questionRepository, IMapper mapper, IRedisCache redisCache, IVoteCache voteCache)
        {
            _voteRepository = voteRepository;
            _uWork = uWork;
            _questionRepository = questionRepository;
            _mapper = mapper;
            _redisCache = redisCache;
            _voteCache = voteCache;
        }


        #region Add Vote

        [PerformanceBehavior]
        [ValidationBehavior(typeof(CreateVoteValidator))]
        public async Task AddVote(AddVoteDto addVote)
        {
            await CheckUser(addVote);
            await CheckQuestion(addVote);

            if (addVote.Voted == null)
            {
                throw new ClientSideException("User can not vote null");
            }

            var vote = await _voteCache.GetVote(addVote.QuestionId, addVote.UserId);
            string cacheKey = string.Format(CacheKeys.GetVoteKey, addVote.QuestionId, addVote.UserId);
            if (vote == null)
            {
                var mapVote = _mapper.Map<Vote>(addVote);
                await _voteRepository.AddAsync(mapVote);
            }
            else
            {
                if (vote.Voted == addVote.Voted || addVote.Voted == null)
                {
                    throw new ClientSideException("aynı vote veya null gelme durumu");
                }

                vote.Voted = vote.Voted == null ? addVote.Voted : null;
                await _voteRepository.UpdateAsync(vote);

            }
            await _voteCache.Remove(cacheKey);
        }

        #endregion

        #region Delete Vote

        [ValidationBehavior(typeof(DeleteVoteValidator))]
        public async Task DeleteVote(DeleteVoteVM deleteVoteVM)
        {
            var deleteVote = await _questionRepository.GetByIdAsync(deleteVoteVM.VoteId);
            while (deleteVote != null)
            { throw new ClientSideException("Böyle bir oy yok"); };
            await _questionRepository.RemoveAsync(deleteVote);
        }

        #endregion

        #region Checking Sections

        private async Task CheckUser(AddVoteDto addVoteDto)
        {
            var user = await _uWork.GetRepository<Account>().GetById(addVoteDto.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
        }
        private async Task CheckQuestion(AddVoteDto addVoteDto)
        {
            var question = await _questionRepository.GetByIdAsync(addVoteDto.QuestionId);
            if (question == null)
            {
                throw new NotFoundException("Question not found");
            }
        }

        #endregion
    }
}
