using Book_Sales_Project.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book_Sales_Project.Controllers
{
    [AllowAnonymous]  
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;


        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserLoginViewModel p)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(p.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, p.Password, false, false);

                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);

                        if (userRoles.Contains("Admin"))
                        {
                            return RedirectToAction("AddBook", "Admin");
                        }
                        else if (userRoles.Contains("User"))
                        {
                            return RedirectToAction("Index", "User");
                        }


                    }
                }

                ModelState.AddModelError("", "Hatalı E-posta veya şifre");
            }

            return View(p);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
