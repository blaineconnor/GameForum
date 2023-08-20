using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;
using Game.Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Forum.Persistence.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        private readonly DbSet<Answer> _dbSet;

        public AnswerRepository(GameForumContext context) : base(context)
        {
            _dbSet = context.Set<Answer>();
        }

        
    }
}
