using Game.Forum.UI.Models.DTOs.Category;
using Game.Forum.UI.Models.Wrapper;
using Game.Forum.UI.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Game.Forum.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRestService _restService;

        public CategoryController(IRestService restService)
        {
            _restService = restService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {

            var response = await _restService.GetAsync<Result<List<CategoryDto>>>("category/get");

            return View(response.Data.Data);
        }

    }
}
