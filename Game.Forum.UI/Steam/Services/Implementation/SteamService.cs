using Game.Forum.UI.Steam.CustomTypes;
using Game.Forum.UI.Steam.Enumerations;
using Game.Forum.UI.Steam.Services.Abstraction;
using System.Net;

namespace Game.Forum.UI.Steam.Services.Implementation
{
    public class SteamService : ISteamService
    {
        private readonly ICultureService _cultureService;

        public SteamService(ICultureService cultureService)
        {
            _cultureService = cultureService;
        }

        public List<Listing> GetListings(string game, Culture culture)
        {
            List<Listing> results = new List<Listing>();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var cultureInfo = _cultureService.GetCultureValue(culture);
            string response = new WebClient().DownloadString($"https://store.steampowered.com/search/suggest?term={game}&f=games&cc={cultureInfo.CultureShortName}&lang={cultureInfo.CultureName}&v=2286217");

            if (!response.Contains("match ds_collapse_flag "))
                return results;

            foreach (string s in response.Split(new string[] { "match ds_collapse_flag " }, StringSplitOptions.None))
                if (s.Contains("match_name") && s.Contains("match_price"))
                    results.Add(new Listing(s, cultureInfo.CultureInfo));

            return results;
        }
    }
}
