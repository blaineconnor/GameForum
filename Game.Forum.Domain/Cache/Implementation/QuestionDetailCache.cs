using AutoMapper;
using Game.Forum.Domain.Cache.Abstraction;
using Game.Forum.Domain.Cache.Keys;
using Game.Forum.Domain.Cache.Redis;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Game.Forum.Domain.Cache.Implementation
{
    public class QuestionDetailCache : RedisCacheManager, IQuestionDetailCache
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IFavoriteCache _favoriteCache;
        private readonly IMapper _mapper;
        public QuestionDetailCache(IDistributedCache distributedCache, IQuestionRepository questionRepository, IFavoriteCache favoriteCache, IMapper mapper) : base(distributedCache)
        {
            _questionRepository = questionRepository;
            _favoriteCache = favoriteCache;
            _mapper = mapper;
        }

        public async Task<QuestionDetailResponse> GetQuestionsWithDetail(int id)
        {
            string cacheKey = string.Format(CacheKeys.QuestionDetailCacheKey, id);
            if (await Any(cacheKey))
            {
                var questionFromCache = await Get<QuestionDetailResponse>(cacheKey);
                return questionFromCache;
            }
            var questionDb = await _questionRepository.GetQuestionsWithDetail(id);
            await Set(cacheKey, questionDb, TimeSpan.FromMinutes(10));
            return questionDb;
        }

        Task<QuestionDetailResponse> IQuestionDetailCache.GetQuestionsWithDetail(int id)
        {
            throw new NotImplementedException();
        }
    }
}
