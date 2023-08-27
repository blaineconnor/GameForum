using Game.Forum.UI.Steam.CustomTypes;
using Game.Forum.UI.Steam.Enumerations;

namespace Game.Forum.UI.Steam.Services.Abstraction
{
    public interface ISteamService
    {

        List<Listing> GetListings(string game, Culture culture);
    }
}
