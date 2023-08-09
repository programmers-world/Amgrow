using Microsoft.AspNetCore.Mvc;

namespace Stockmantras.Controllers
{
    public class FreeCourse : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
