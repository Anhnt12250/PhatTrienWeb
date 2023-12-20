using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PhatTrienWeb.Data;
using PhatTrienWeb.ViewModels;

namespace Test.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDBContext _dbContext;

        public ProductsController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            // Lấy danh sách các category mà có ít nhất 1 sản phẩm
            var categories = await _dbContext.Products
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            var viewModel = new ProductsViewModel
            {
                Categories = categories 
            };

            return View(viewModel);
        }
    }
}
