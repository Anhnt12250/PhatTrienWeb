using System.ComponentModel.DataAnnotations;

namespace PhatTrienWeb.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} charaters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

    }
}
