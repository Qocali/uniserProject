using Microsoft.AspNetCore.Mvc;

namespace uniserProject.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
