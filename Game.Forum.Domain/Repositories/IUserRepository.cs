using Game.Forum.Domain.Entities;
using System.Linq.Expressions;

namespace Game.Forum.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<bool> CommitAsync();
        Task<User> GetUserByEmail(string email);
        Task<User> Login(string email, string password);
    }
}
