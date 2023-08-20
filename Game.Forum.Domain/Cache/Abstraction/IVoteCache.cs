using Game.Forum.Domain.Cache.Redis;
using Game.Forum.Domain.Entities;

namespace Game.Forum.Domain.Cache.Abstraction
{
    public interface IVoteCache : IRedisCache
    {
        public Task<int> GetNumberOfVotes(int questionId);
        public Task<Vote> GetVote(int questionId, int userId);
    }
}
