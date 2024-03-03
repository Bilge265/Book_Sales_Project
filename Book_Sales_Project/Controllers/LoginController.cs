using Book_Sales_Project.Models;
using BusinessLayer.ValidationRules;
using EntityLayer.Identity;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
                                return RedirectToAction("Index", "Home");
                            }

						
					}
                    }
              
                    ModelState.AddModelError("", "E-postanız veya şifreniz yanlış.");
                
            }
            return View(p);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
          
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


    }
}
