using Game.Forum.Domain.Cache.Redis;

namespace Game.Forum.Domain.Cache.Abstraction
{
    public interface IFavoriteCache : IRedisCache
    {
        public Task<bool> CheckFav(int id, int userId);
        public Task RemoveFavoriteCache(int id, int userId);
    }
}
