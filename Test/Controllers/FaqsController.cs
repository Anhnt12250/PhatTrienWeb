using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class FaqsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
