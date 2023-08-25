using AutoMapper;
using Game.Forum.UI.Models;
using Game.Forum.UI.Models.RequestModels.Answers;
using Game.Forum.UI.Models.RequestModels.Question;
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
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswer(AddAnswerVM addAnswerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(addAnswerVM);
            }
            var response = await _restService.GetAsync<Result<List<AddAnswerVM>>>("answer/details");

            return View(response.Data.Data);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAnswer(DeleteAnswerVM deleteAnswerVM)
        {
            var response = await _restService.DeleteAsync<Result<int>>($"answer/delete");

            return Json(response.Data);

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddQuestion(AddQuestionVM addQuestionVM)
        {
            if (!ModelState.IsValid)
            {
                return View(addQuestionVM);
            }
            var response = await _restService.GetAsync<Result<List<AddQuestionVM>>>("question/details");
            return View(response.Data.Data);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}