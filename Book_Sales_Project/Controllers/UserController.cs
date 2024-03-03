﻿using Book_Sales_Project.Models;
using BusinessLayer.Abstract;
using EntityLayer.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book_Sales_Project.Controllers
{
    [Authorize(Roles = "User")]
	public class UserController : Controller
	{
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var values = _userService.TGetByID(user.Id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUser appUser)
        {
           
                var user = await _userManager.GetUserAsync(HttpContext.User);

                user.Name = appUser.Name;
                user.Surname = appUser.Surname;
                user.PhoneNumber = appUser.PhoneNumber;
                user.Email = appUser.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {                 
                    return RedirectToAction("Index","User");
                }
                else
                {
                   
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Güncelleme işlemi gerçekleşmedi!", error.Description);
                    }
                    return View(appUser);
                }
            
          
        }
        [HttpGet]
        public IActionResult UserPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserPassword(PasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
 
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
          
                    TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirildi.";
                    return RedirectToAction("UserPassword", "User");
                }
                else
                {
  
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }
            else
            {     
                return View(model);
            }
        }

    }
}
