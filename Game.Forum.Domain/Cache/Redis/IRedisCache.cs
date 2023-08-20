namespace Game.Forum.Domain.Cache.Redis
{
    public interface IRedisCache
    {
        Task<byte[]> Get(string key);
        Task<T> Get<T>(string key);
        Task Set(string key, object value, TimeSpan timeSpan);
        Task Refresh(string key);
        Task<bool> Any(string key);
        Task Remove(string key);

    }
}
