using Book_Sales_Project.Models;
using EntityLayer.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Book_Sales_Project.Controllers
{

    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new UserRegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserRegisterViewModel p)
        {

            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {

                    Name = p.Name,
                    Surname = p.Surname,
                    Email = p.Email,
                    UserName = p.Name + p.Surname,
                   
            };

                var result = await _userManager.CreateAsync(appUser, p.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "User");

                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                }

            }

            return View(p);
        }
    }
}
