using Microsoft.AspNetCore.Mvc;
using PhatTrienWeb.Data;
using PhatTrienWeb.ViewModels;

namespace PhatTrienWeb.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly AppDBContext _dbContext;
        public ProductDetailController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int id)
        {
            var product = _dbContext.Products.Find(id);
            var otherProducts = _dbContext.Products
                .Where(p => p.Id != id)
                .Take(3)
                .ToList();

            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                OtherProducts = otherProducts
            };  

            return View(viewModel);
        }
    }
}
