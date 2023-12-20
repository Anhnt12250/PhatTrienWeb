using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhatTrienWeb.Data;

namespace Test.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly AppDBContext _dbConext;
        private readonly SignInManager<AppUser> _signInManager;

        public CheckOutController(AppDBContext dbConext, SignInManager<AppUser> signInManager)
        {
            _dbConext = dbConext;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _signInManager.UserManager.GetUserId(HttpContext.User);

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var order = _dbConext.Orders.Where(o => o.UserId == userId).FirstOrDefault();

            if (order == null)
            {
                return View();
            }

            var orderProducts = await _dbConext.OrderProducts
                .Include(op => op.Product)
                .Where(op => op.OrderId == order.Id)
                .ToListAsync();

            double totalPrice = 0;

            foreach (var orderProduct in orderProducts)
            {
                totalPrice += orderProduct.Product!.Price * orderProduct.Quantity ?? 0;
            }

            return View(totalPrice);
        }

        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
