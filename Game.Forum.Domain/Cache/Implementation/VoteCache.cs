using AutoMapper;
using Game.Forum.Domain.Cache.Abstraction;
using Game.Forum.Domain.Cache.Keys;
using Game.Forum.Domain.Cache.Redis;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Game.Forum.Domain.Cache.Implementation
{
    public class VoteCache : RedisCacheManager, IVoteCache
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IMapper _mapper;
        public VoteCache(IDistributedCache distributedCache, IVoteRepository voteRepository, IMapper mapper) : base(distributedCache)
        {
            _voteRepository = voteRepository;
            _mapper = mapper;
        }

        public async Task<int> GetNumberOfVotes(int questionId)
        {
            string cacheKey = string.Format(CacheKeys.NumberOfVoteKey, questionId);
            if (await Any(cacheKey))
            {
                return await Get<int>(cacheKey);
            }
            var numberOfVoted = await _voteRepository.GetNumberOfVotes(questionId);
            await Set(cacheKey, numberOfVoted, TimeSpan.FromMinutes(10));
            return numberOfVoted;
        }

        public async Task<Vote> GetVote(int questionId, int userId)
        {
            string cacheKey = string.Format(CacheKeys.GetVoteKey, questionId, userId);
            if (await Any(cacheKey))
            {
                return await Get<Vote>(cacheKey);
            }
            var vote = await _voteRepository.GetVote(questionId, userId);
            await Set(cacheKey, vote, TimeSpan.FromMinutes(10));
            return vote;

        }

    }
}
