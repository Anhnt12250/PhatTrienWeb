using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhatTrienWeb.Data;

namespace PhatTrienWeb.Components
{
    public class ProductListViewComponent : ViewComponent
    {
        private readonly AppDBContext _dbContext;

        public ProductListViewComponent(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? categoryId)
        {
            if (categoryId == null)
            {
                var products = await _dbContext.Products.ToListAsync();
                return View(products);
            } else
            {
                var products = await _dbContext.Products
                                .Where(p => p.CategoryId == categoryId)
                                .ToListAsync();
                return View(products);
            }
        }
    }
}
