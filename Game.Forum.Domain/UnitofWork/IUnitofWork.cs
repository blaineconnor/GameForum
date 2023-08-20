using Game.Forum.Domain.Common;
using Game.Forum.Domain.Repositories;

namespace Game.Forum.Domain.UnitofWork
{
    public interface IUnitofWork : IDisposable
    {
        public IRepository<T> GetRepository<T>() where T : BaseEntity, IEntity, ISoftDelete, IHasUpdatedAt;
        public Task<bool> CommitAsync();
    }
}
