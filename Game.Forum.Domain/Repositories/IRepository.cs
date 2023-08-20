using Game.Forum.Domain.Common;
using Game.Forum.Domain.Entities;
using System.Linq.Expressions;

namespace Game.Forum.Domain.Repositories
{
    public interface IRepository<T> where T : BaseEntity, IEntity, ISoftDelete, IHasUpdatedAt
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task<T> GetById(object id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Delete(object id);
        Task<IQueryable<T>> GetByFilterAsync(Func<User, bool> value, Expression<Func<T, bool>> filter);
        Task<User> GetByFilterAsync(Func<User, bool> value, string v);
    }
}
