using Book_Sales_Project.Models.BasketViewModels;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using EntityLayer.Identity;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book_Sales_Project.Controllers
{
	[Authorize(Roles = "User")]
	public class BasketController : Controller
	{
		private readonly IBasketService _basketService;
		private readonly IBasketItemService _basketItemService;
		private readonly UserManager<AppUser> _userManager;

		public BasketController(IBasketService basketService, UserManager<AppUser> userManager, IBasketItemService basketItemService)
		{
			_basketService = basketService;
			_userManager = userManager;
			_basketItemService = basketItemService;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			var basketItems = _basketService.TGetAllBasketItemsByBasketId(user.Id);

			return View(basketItems);
		}

		[HttpPost]
		public async Task<IActionResult> AddToBasket(BasketItem basketItem)
		{
			var basketItems = _basketItemService.TGetList();
			var baskets = _basketService.TGetByID(1);
			return View();
		}

	}
}
