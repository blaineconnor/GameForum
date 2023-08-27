using AutoMapper;
using Game.Forum.UI.Models.RequestModels.Answers;
using Game.Forum.UI.Models.Wrapper;
using Game.Forum.UI.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Game.Forum.UI.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IRestService _restService;

        public AnswerController(IRestService restService, IMapper mapper)
        {
            _restService = restService;

        }

        public IActionResult AddAnswer()
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
            var response = await _restService.PostAsync<Result<List<AddAnswerVM>>>("question/details");

            return View(addAnswerVM);
        }
    }
}
