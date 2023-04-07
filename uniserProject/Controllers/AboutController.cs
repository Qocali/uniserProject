using Microsoft.AspNetCore.Mvc;

namespace uniserProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
