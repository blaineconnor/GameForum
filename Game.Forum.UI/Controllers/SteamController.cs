using AutoMapper;
using Game.Forum.UI.Models.RequestModels.Steam;
using Game.Forum.UI.Services.Abstraction;
using Game.Forum.UI.Steam.CustomTypes;
using Game.Forum.UI.Steam.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Game.Forum.UI.Controllers
{
    public class SteamController : Controller
    {
        private readonly ISteamService _steamService;

        public SteamController(ISteamService steamService)
        {
            _steamService = steamService;

        }
        public IActionResult SteamQuery()
        {
            return View();
        }

 
        //public IActionResult SteamResult()
        //{
        //    return View();
        //}

        //[HttpPost]

        //public IEnumerable<Listing> SteamResult([FromBody] GetSteamGameInfoRequestModel request)
        //{
        //    var result = _steamService.GetListings(request.Game, request.Culture);

        //    return (IEnumerable<Listing>)View(result);
        //}

    }
}
