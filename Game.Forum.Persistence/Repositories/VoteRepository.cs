using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;
using Game.Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Forum.Persistence.Repositories
{
    public class VoteRepository : GenericRepository<Vote>, IVoteRepository
    {
        private readonly DbSet<Vote> _vote;

        public VoteRepository(GameForumContext context) : base(context)
        {
            _vote = context.Set<Vote>();
        }
        public async Task<bool> CheckVote(int questionId, int userId)
        {
            return await _vote.AnyAsync(x => x.QuestionId == questionId && x.Id == userId);
        }
        public async Task<Vote?> GetVote(int questionId, int userId)
        {
            return await _vote.FirstOrDefaultAsync(x => x.QuestionId.Equals(questionId) && x.Id.Equals(userId) );
        }

        public async Task<int> GetNumberOfVotes(int questionId)
        {
            var trueVotes = await _vote.Where(x => x.QuestionId.Equals(questionId)).Where(y => y.Voted==true).CountAsync();
            var falseVotes = await _vote.Where(x => x.QuestionId.Equals(questionId)).Where(y => y.Voted==false).CountAsync();
            return trueVotes - falseVotes;
        }
    }
}
