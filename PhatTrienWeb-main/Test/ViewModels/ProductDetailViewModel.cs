using PhatTrienWeb.Models;

namespace PhatTrienWeb.ViewModels
{
    public class ProductDetailViewModel
    {
        public required Product? Product { get; set; }
        public required List<Product>? OtherProducts { get; set; }
    }
}
