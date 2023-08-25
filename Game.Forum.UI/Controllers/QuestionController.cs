using AutoMapper;
using Game.Forum.UI.Models.RequestModels.Question;
using Game.Forum.UI.Models.Wrapper;
using Game.Forum.UI.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Game.Forum.UI.Controllers
{
    public class QuestionController : Controller
    {
        private IRestService _restService;

        public QuestionController(IRestService restService, IMapper mapper)
        {
            _restService = restService;

        }
        public IActionResult Index()
        {
            return View();
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
    }
}
