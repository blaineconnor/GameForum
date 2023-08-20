using Game.Forum.Domain.Cache.Abstraction;
using Game.Forum.Domain.Cache.Keys;
using Game.Forum.Domain.Cache.Redis;
using Game.Forum.Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Game.Forum.Domain.Cache.Implementation
{
    public class FavoriteCache : RedisCacheManager, IFavoriteCache
    {
        private readonly IFavoriteRepository _favoriteRepository;
        public FavoriteCache(IDistributedCache distributedCache, IFavoriteRepository favoriteRepository) : base(distributedCache)
        {
            _favoriteRepository = favoriteRepository;
        }
        public async Task<bool> CheckFav(int id, int userId)
        {
            string cacheKey = string.Format(CacheKeys.FavoriteCacheKey, id, userId);
            if (await Any(cacheKey))
            {
                return await Get<bool>(cacheKey);
            }
            bool isFavorite = await _favoriteRepository.CheckFavorite(id, userId);
            if (isFavorite)
                await Set(cacheKey, isFavorite, TimeSpan.FromMinutes(10));
            else
                await Set(cacheKey, false, TimeSpan.FromMinutes(10));
            return isFavorite;
        }
        public async Task RemoveFavoriteCache(int id, int userId)
        {
            string cacheKey = string.Format(CacheKeys.FavoriteCacheKey, id, userId);
            await Remove(cacheKey);
        }
    }
}
