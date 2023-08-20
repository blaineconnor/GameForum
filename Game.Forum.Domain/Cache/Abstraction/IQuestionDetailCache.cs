using Game.Forum.Domain.Cache.Redis;
using Game.Forum.Domain.Entities;

namespace Game.Forum.Domain.Cache.Abstraction
{
    public interface IQuestionDetailCache : IRedisCache
    {
        public Task<QuestionDetailResponse> GetQuestionsWithDetail(int id);

    }
}
