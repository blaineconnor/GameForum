using Game.Forum.UI.Steam.Enumerations;

namespace Game.Forum.UI.Models.RequestModels.Steam
{
    public class GetSteamGameInfoRequestModel
    {
        public string Game { get; set; }
        public Culture Culture { get; set; }
    }
}
