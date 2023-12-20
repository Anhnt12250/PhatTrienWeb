using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using PhatTrienWeb.Data;
using PhatTrienWeb.Models;

namespace PhatTrienWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly AppDBContext _dbConext;
        private readonly SignInManager<AppUser> _signInManager;

        public ShoppingCartController(AppDBContext dbConext, SignInManager<AppUser> signInManager)
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
            
            return View(orderProducts);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity([FromForm] int productId, [FromForm] int quantity)
        {
            System.Diagnostics.Debug.WriteLine("productId: " + productId);
            System.Diagnostics.Debug.WriteLine("quantity: " + quantity);

            var userId = _signInManager.UserManager.GetUserId(HttpContext.User);
            var order = _dbConext.Orders.Where(o => o.UserId == userId).FirstOrDefault();

            if (order == null)
            {
                return Content("Giỏ hàng của bạn đang trống");
            }

            var orderProduct = await _dbConext.OrderProducts
                .Where(op => op.OrderId == order.Id && op.ProductId == productId)
                .FirstOrDefaultAsync();

            if (orderProduct == null)
            {
                return Content("Sản phẩm không tồn tại trong giỏ hàng");
            }

            orderProduct.Quantity = quantity;
            await _dbConext.SaveChangesAsync();

            return RedirectToAction("Index");
        }   

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var userId = _signInManager.UserManager.GetUserId(HttpContext.User);
            var order = _dbConext.Orders.Where(o => o.UserId == userId).FirstOrDefault();

            if (order == null)
            {
                return Content("Giỏ hàng của bạn đang trống");
            }

            var orderProduct = await _dbConext.OrderProducts
                .Where(op => op.OrderId == order.Id && op.ProductId == id)
                .FirstOrDefaultAsync();

            if (orderProduct == null)
            {
                return Content("Sản phẩm không tồn tại trong giỏ hàng");
            }

            _dbConext.OrderProducts.Remove(orderProduct);
            await _dbConext.SaveChangesAsync();

            return RedirectToAction("Index");
        }   

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            if (_signInManager.IsSignedIn(HttpContext.User)) 
            { 
                var userId = _signInManager.UserManager.GetUserId(HttpContext.User);

                if (userId == null)
                {
                    return Content("Bạn cần đăng nhập để thêm vào giỏ hàng");
                }

                var cart = _dbConext.Orders.Where(o => o.UserId == userId).FirstOrDefault();

                if (cart == null)
                {
                    cart = new Order
                    {
                        UserId = userId
                    };

                    _dbConext.Orders.Add(cart);
                    _dbConext.SaveChanges();
                }

                var product = _dbConext.Products.Find(id);

                if (product == null)
                {
                    return Content("Sản phẩm không tồn tại");
                }


                var orderProduct = _dbConext.OrderProducts.Where(op => op.OrderId == cart.Id && op.ProductId == product.Id).FirstOrDefault();
                if (orderProduct == null)
                {
                    orderProduct = new OrderProduct
                    {
                        OrderId = cart.Id,
                        ProductId = product.Id,
                        Quantity = 1,
                        Price = product.Price
                    };
                    _dbConext.OrderProducts.Add(orderProduct);
                }
                else
                {
                    orderProduct.Quantity += 1;
                }

                _dbConext.SaveChanges();

                return Content("Đã thêm vào giỏ hàng");
            }
            else
            {
                return Content("Bạn cần đăng nhập để thêm vào giỏ hàng");
            }
        }
    }
}
