using Microsoft.AspNetCore.Mvc;

namespace uniserProject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
