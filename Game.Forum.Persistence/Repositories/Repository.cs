using Game.Forum.Domain.Common;
using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;
using Game.Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Game.Forum.Persistence.Repositories
{
    public class Repository<T> : IRepository<T>
            where T : BaseEntity, IEntity, ISoftDelete, IHasUpdatedAt
    {
        private readonly GameForumContext _dbContext;

        public Repository(GameForumContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Set<T>());
        }



        public async Task<T> GetById(object id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().AnyAsync(filter);
        }


        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(object id)
        {
            await _dbContext.Set<T>().FindAsync(id);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IQueryable<T>> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await Task.FromResult(_dbContext.Set<T>().Where(filter));
        }

        public async Task<IQueryable<T>> GetByFilterAsync(Func<User, bool> value, Expression<Func<T, bool>> filter)
        {
            return await Task.FromResult(_dbContext.Set<T>().Where(filter));
        }

        public async Task<User> GetByFilterAsync(Func<User, bool> value, string v)
        {
            IQueryable<User> query = _dbContext.Set<User>();

            // Eğer include parametresi varsa, ilişkili nesneleri de yükle
            if (!string.IsNullOrEmpty( v))
            {
                query = query.Include( v);
            }

            // Filtre fonksiyonunu uygula ve ilk eşleşen User nesnesini döndür
            return await query.FirstOrDefaultAsync( );
        }
    }
}


