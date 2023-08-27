using AutoMapper;
using Game.Forum.UI.Models;
using Game.Forum.UI.Models.RequestModels.Answers;
using Game.Forum.UI.Models.RequestModels.Question;
using Game.Forum.UI.Models.RequestModels.User;
using Game.Forum.UI.Models.Wrapper;
using Game.Forum.UI.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Game.Forum.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestService _restService;


        public HomeController(ILogger<HomeController> logger, IRestService restService, IMapper mapper)
        {
            _logger = logger;
            _restService = restService;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(UserContactVM userContactVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userContactVM);
            }
            var response = await _restService.PostAsync<Result<List<UserContactVM>>>("index/contact");

            return View(userContactVM);
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}