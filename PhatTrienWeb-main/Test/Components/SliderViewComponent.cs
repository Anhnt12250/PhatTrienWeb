using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhatTrienWeb.Data;
using PhatTrienWeb.Models;

namespace PhatTrienWeb.Components
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly AppDBContext _dbContext;

        public SliderViewComponent(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? categoryId)
        {
            if (categoryId == null)
            {
                var products = await _dbContext.Products.Take(7).ToListAsync();
                return View(products);
            }
            else
            {
                var products = await _dbContext.Products
                                .Where(p => p.CategoryId == categoryId)
                                .ToListAsync();
                return View(products);
            }
        }
    }
}
