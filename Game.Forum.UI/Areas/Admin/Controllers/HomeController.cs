using Microsoft.AspNetCore.Mvc;

namespace Game.Forum.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Header = "Game Gourmet Forum";
            ViewBag.Title = "Yönetim Paneli";
            return View();
        }
    }
}
