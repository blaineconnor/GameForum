using Game.Forum.Infrastructure.Steam.Enumerations;

namespace Game.Forum.Api.Models
{
    public class GetSteamGameInfoRequestModel
    {
        public string Game { get; set; }
        public Culture Culture { get; set; }
    }
}
