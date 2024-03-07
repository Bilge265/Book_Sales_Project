using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using EntityLayer.Identity;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Book_Sales_Project.Controllers
{
	[AllowAnonymous]
	public class BookDetailController : Controller
	{
		private readonly IBookService _bookService;
		private readonly IBasketService _basketService;
		private readonly IBasketItemService _basketItemService;
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public BookDetailController(IBookService bookService, IBasketService basketService, IBasketItemService basketItemService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_bookService = bookService;
			_basketService = basketService;
			_basketItemService = basketItemService;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Index(int id)
		{
			var values = _bookService.TGetByID(id);
			return View(values);
		}

		[HttpPost]
		public async Task<IActionResult> AddToBasket(BasketItem values, int quantity)
		{
			if (!_signInManager.IsSignedIn(User))
			{
				return RedirectToAction("Index", "Login");
			}

			var userId = _userManager.GetUserId(HttpContext.User);

			BasketItemValidator validator = new BasketItemValidator();
			ValidationResult validationResult = validator.Validate(values);
			if (validationResult.IsValid)
			{
				var existingItem = _basketItemService.TGetByID(values.BookId);

				if (existingItem != null)
				{
					// Aynı kitap zaten sepete eklenmişse, miktarını artır
					existingItem.Quantity += quantity;
					_basketItemService.TUpdate(existingItem);
				}
				else
				{
					var basket = new Basket
					{
						CustomerId = int.Parse(userId),
						BasketItems = new List<BasketItem> { values }
					};

					_basketService.TAdd(basket);
				}
				return RedirectToAction("Index", "Basket");
			}
			else
			{
				foreach (var error in validationResult.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
			}

			return View();
		}


	}
}
