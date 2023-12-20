using Microsoft.AspNetCore.Mvc;
using PhatTrienWeb.Models;
using PhatTrienWeb.ViewModels;

namespace PhatTrienWeb.Components
{
    public class ProductViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Product product, bool? isLast)
        {
            if (product == null)
            {
                return await Task.FromResult<IViewComponentResult>(Content("Không có sản phẩm nào"));
            }

            var viewModel = new ProductViewModel
            {
                Product = product,
                IsLast = isLast ?? false
            };

            return View(viewModel);
        }

    }
}
