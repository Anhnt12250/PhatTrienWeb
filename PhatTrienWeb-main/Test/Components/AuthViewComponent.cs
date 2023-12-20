using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhatTrienWeb.Data;

namespace PhatTrienWeb.Components
{
    public class AuthViewComponent : ViewComponent
    {
        private readonly SignInManager<AppUser> _signInManager;
        public AuthViewComponent(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var isSignedIn = _signInManager.IsSignedIn(HttpContext.User);
            return View(isSignedIn);
        }
    }
}
