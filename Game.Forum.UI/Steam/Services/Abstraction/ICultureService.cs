using Game.Forum.UI.Steam.CustomTypes;
using Game.Forum.UI.Steam.Enumerations;

namespace Game.Forum.UI.Steam.Services.Abstraction
{
    public interface ICultureService
    {
        CultureValue GetCultureValue(Culture culture);
    }
}
