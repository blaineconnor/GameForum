using Game.Forum.Domain.Common;
using Game.Forum.Domain.Repositories;
using Game.Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Game.Forum.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        protected readonly GameForumContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(GameForumContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();

        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {

            return await Task.FromResult(_dbSet.Where(expression));

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(_dbSet.ToList());
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public  IRepository<T1> GetRepository<T1>() where T1 : BaseEntity, IEntity, ISoftDelete, IHasUpdatedAt
        {
            return (IRepository<T1>)Task.FromResult(_dbSet.ToList());
        }

        

        public async Task RemoveAsync(T entity, bool hardDelete = false)
        {
            if (entity is ISoftDelete soft && !hardDelete)
            {
                soft.IsDeleted = true;
                await UpdateAsync(entity);
            }
            else
            {
                await Task.FromResult(_dbSet.Remove(entity));
            }
            await SaveChanges();
        }

        public async Task RemoveAsync(object id, bool hardDelete = false)
        {
            var entity = await GetByIdAsync(id);

            if (entity==null)
            {
                throw new ArgumentNullException("Kayıt bulunamadı");
            }

            await RemoveAsync(entity, hardDelete);
        }

        public async Task UpdateAsync(IEntity _entity)
        {
            var entity = await GetByIdAsync(_entity.Id);

            if (entity == null)
            {
                throw new Exception("");
            }
            if (entity is IHasUpdatedAt at)
            {
                at.UpdatedTime = DateTime.Now;
            }
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.FromResult(_dbSet.Update(entity));
            await SaveChanges();
            //Task.From result kıllanarak async olmayan metodu async hale getiriyoruz
            //await _dbSet.Update(entity);
        }
        private async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
