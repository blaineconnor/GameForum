using Game.Forum.Infrastructure.Steam.CustomTypes;
using Game.Forum.Infrastructure.Steam.Enumerations;

namespace Game.Forum.Infrastructure.Steam.Services.Abstraction
{
    public interface ISteamService
    {

        List<Listing> GetListings(string game, Culture culture);
    }
}
