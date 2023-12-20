using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhatTrienWeb.Data;

namespace PhatTrienWeb.Components
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly AppDBContext _dbContext;

        public SidebarViewComponent(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _dbContext.Categories.ToListAsync();

            return View(categories);
        }
    }
}
