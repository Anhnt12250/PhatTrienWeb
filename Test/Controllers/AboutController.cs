using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
