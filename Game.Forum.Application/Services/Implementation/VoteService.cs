using AutoMapper;
using Game.Forum.Application.Exceptions;
using Game.Forum.Application.Models.DTOs.Vote;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Domain.Cache.Abstraction;
using Game.Forum.Domain.Cache.Keys;
using Game.Forum.Domain.Cache.Redis;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;

namespace Game.Forum.Application.Services.Implementation
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IRedisCache _redisCache;
        private readonly IVoteCache _voteCache;

        public VoteService(IVoteRepository voteRepository, IUserRepository userRepository, IQuestionRepository questionRepository, IMapper mapper, IRedisCache redisCache, IVoteCache voteCache)
        {
            _voteRepository = voteRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
            _redisCache = redisCache;
            _voteCache = voteCache;
        }

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

        private async Task CheckUser(AddVoteDto addVoteContract)
        {
            var user = await _userRepository.GetByIdAsync(addVoteContract.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
        }
        private async Task CheckQuestion(AddVoteDto addVoteContract)
        {
            var question = await _questionRepository.GetByIdAsync(addVoteContract.QuestionId);
            if (question == null)
            {
                throw new NotFoundException("Question not found");
            }
        }
    }
}
