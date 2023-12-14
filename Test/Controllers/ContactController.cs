using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
