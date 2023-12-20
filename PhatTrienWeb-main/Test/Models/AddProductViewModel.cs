using System.ComponentModel.DataAnnotations;

namespace PhatTrienWeb.Models
{
    public class AddProductViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} charaters long.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = null!;

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Image")]
        public string Image { get; set; } = null!;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Category")]
        public string CategoryId { get; set; } = null!;

    }
}
