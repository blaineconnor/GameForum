using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;
using Game.Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Forum.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _dbSet;

        private readonly GameForumContext _context;
        public UserRepository(GameForumContext context) : base(context)
        {
            _dbSet = context.Set<User>();
        }

        public async Task<bool> CommitAsync()
        {
            var result = false;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    result = true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(h => h.Email == email);
        }

        public async Task<User> Login(string email, string password)
        {
            return await _dbSet.FirstOrDefaultAsync(h => h.Email == email && h.Password == password);
        }

    }
}
