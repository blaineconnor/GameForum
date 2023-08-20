using Game.Forum.Domain.Entities;

namespace Game.Forum.Domain.Repositories
{
    public interface IVoteRepository : IGenericRepository<Vote>
    {
        Task<bool> CheckVote(int questionId, int userId);
        Task<Vote?> GetVote(int questionId, int userId);
        Task<int> GetNumberOfVotes(int questionId);

    }
}
