using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhatTrienWeb.Data;
using PhatTrienWeb.Models;
using PhatTrienWeb.ViewModels;

namespace PhatTrienWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDBContext _dbConext;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminController(AppDBContext dbConext, SignInManager<AppUser> signInManager)
        {
            _dbConext = dbConext;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var userId = _signInManager.UserManager.GetUserId(HttpContext.User);

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var products = _dbConext.Products.ToList();

            var viewModel = new AdminViewModel
            {
                Products = products
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(AddProductViewModel productModel)
        {
            if(ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = productModel.ProductName,
                    Price = productModel.Price,
                    Description = productModel.Description,
                    Image = productModel.Image,
                    CategoryId = Convert.ToInt32(productModel.CategoryId)
                };

                _dbConext.Products.Add(product);
                _dbConext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = _dbConext.Products.Find(id);

            if(product != null)
            {
                _dbConext.Products.Remove(product);
                _dbConext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
