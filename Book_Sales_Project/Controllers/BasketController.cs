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
		private readonly IBookService _bookService;
		private readonly UserManager<AppUser> _userManager;

		public BasketController(IBasketService basketService, IBasketItemService basketItemService, IBookService bookService, UserManager<AppUser> userManager)
		{
			_basketService = basketService;
			_basketItemService = basketItemService;
			_bookService = bookService;
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			var userBasket = _basketService.TGetUserBasket(user.Id);
			var basketItems = _basketService.TGetAllBasketItemsByBasketId(userBasket.Id);
		
			var viewModel = new BasketViewModel
			{
				BasketItems = basketItems,
				Baskets = userBasket,
			};
			return View(viewModel);
		}
		[HttpPost]
		public async Task<IActionResult> AddToBasket(int bookId, int quantity)
		{
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userBasket = _basketService.TGetUserBasket(user.Id);
			var existingItem = _basketItemService.TGetByBasketIdAndBookId(userBasket.Id, bookId);

			if (existingItem != null)
			{
				existingItem.Quantity += quantity;
				_basketItemService.TUpdate(existingItem);
			}
			else
			{
				var book = _bookService.TGetBookById(bookId);
				var productTotalPrice = book.Price * quantity;

				var basketItem = new BasketItem
				{
					BookId = bookId,
					Quantity = quantity,
					ProductTotalPrice = productTotalPrice,
					BasketId = userBasket.Id
				};

				_basketItemService.TAdd(basketItem);
			}
			var updatedBasketItems = _basketService.TGetAllBasketItemsByBasketId(userBasket.Id);
			decimal totalPrice = updatedBasketItems.Sum(item => item.ProductTotalPrice);

			userBasket.TotalPrice = totalPrice;
			_basketService.TUpdate(userBasket);

			return RedirectToAction("Index", "Basket");
        }
	}
}
