using Game.Forum.Domain.Repositories;
using Game.Forum.Domain.UnitofWork;
using Game.Forum.Persistence.Context;
using Game.Forum.Persistence.Repositories;

namespace Game.Forum.Persistence.UnitofWork
{
    public class UnitWork : IUnitofWork
    {
        private Dictionary<Type, object> _repositories;
        private readonly GameForumContext _context;

        public UnitWork(GameForumContext context)
        {
            _repositories = new Dictionary<Type, object>();
            _context = context;
        }

        #region Kayıtlı Tüm Repository'leri çalıştır.
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
        #endregion


        #region Dependency Injection içerisinde kayıtlı olan ilgili entity için kullanılabilecek Repository
        IRepository<T> IUnitofWork.GetRepository<T>()
        {
            //Daha önce bu repoyu talep eden bir kullanıcı olmuşsa aynı repoyu tekrar üretmez.
            //Burada sakladığı koleksiyon içerisinden gönderir. Bu da performansı artırır.
            if (_repositories.ContainsKey(typeof(IRepository<T>)))
            {
                return (IRepository<T>)_repositories[typeof(IRepository<T>)];
            }

            var repository = new Repository<T>(_context);
            _repositories.Add(typeof(IRepository<T>), repository);
            return repository;
        }
        #endregion


        #region Dispose : Yönetilmeyen kaynakları serbest bırakmak için kullanılan yöntem

        bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        #endregion
    }
}
