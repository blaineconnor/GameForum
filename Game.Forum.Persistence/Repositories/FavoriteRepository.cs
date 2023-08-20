using Game.Forum.Domain.Entities;
using Game.Forum.Domain.Repositories;
using Game.Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Forum.Persistence.Repositories
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        private readonly DbSet<Favorite> _favorite;

        public FavoriteRepository(GameForumContext context) : base(context)
        {
            _favorite = context.Set<Favorite>();
        }

        public async Task<bool> CheckFavorite(int questionId, int userId)
        {
            var isFavorited = await _favorite.AnyAsync(x => x.UserId == userId && x.QuestionId == questionId);
            return isFavorited;

        }


    }
}
