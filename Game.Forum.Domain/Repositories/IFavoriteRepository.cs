using Game.Forum.Domain.Entities;

namespace Game.Forum.Domain.Repositories
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<bool> CheckFavorite(int questionId, int userId);
    }
}
