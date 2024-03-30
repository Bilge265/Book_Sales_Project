using Book_Sales_Project.Models.BasketViewModels;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using EntityLayer.Identity;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Sales_Project.Controllers
{
	[Authorize(Roles = "User")]
	public class BasketController : Controller
	{
		private readonly IBasketService _basketService;
		private readonly IBasketItemService _basketItemService;
		private readonly IOrderItemService _orderItemService;
		private readonly IBookService _bookService;
		private readonly IOrderService _orderService;
		private readonly UserManager<AppUser> _userManager;

		public BasketController(IBasketService basketService, IBasketItemService basketItemService, IOrderItemService orderItemService, IBookService bookService, IOrderService orderService, UserManager<AppUser> userManager)
		{
			_basketService = basketService;
			_basketItemService = basketItemService;
			_orderItemService = orderItemService;
			_bookService = bookService;
			_orderService = orderService;
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
				var book = _bookService.TGetBookById(bookId);
				existingItem.Quantity += quantity;
				existingItem.ProductTotalPrice = book.Price * existingItem.Quantity;
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
		[HttpPost]
		public IActionResult DeleteBasket(int bookId)
		{
			_basketItemService.TRemoveItemFromBasket(bookId);
			return RedirectToAction("Index", "Basket"); 
		}

        [HttpPost]
        public async Task<IActionResult> DeleteBasketItem()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userBasket = _basketService.TGetUserBasket(user.Id);
            var basketItems = _basketService.TGetAllBasketItemsByBasketId(userBasket.Id);
			decimal totalPrice = userBasket.TotalPrice;

			var order = new Order
			{
				CustomerId= user.Id,
				OrderDate = DateTime.Now,
				TotalPrice = totalPrice,
	
			};

			_orderService.TAdd(order);


			foreach (var basketItem in userBasket.BasketItems)
			{
				var orderItem = new OrderItem
				{
					Quantity = basketItem.Quantity,
					BookId = basketItem.BookId,
				};
			
				_orderItemService.TAdd(orderItem);
			}


			//if (basketItems != null)
			//         {

			//             foreach (var basketItem in basketItems)
			//             {
			//                 _basketItemService.TDelete(basketItem);
			//             }
			//             var updatedBasketItems = _basketService.TGetAllBasketItemsByBasketId(userBasket.Id);
			//             decimal totalPrice2 = updatedBasketItems.Sum(item => item.ProductTotalPrice);

			//             userBasket.TotalPrice = totalPrice2;
			//             _basketService.TUpdate(userBasket);
			//             return RedirectToAction("Index", "Order");
			//         }
			//         else
			//         {
			//             TempData["ErrorMessage"] = "Kullanıcıya ait bir sepet bulunamadı.";
			//             return RedirectToAction("Index", "Basket");
			//         }
			return RedirectToAction("Index", "Order");
		}

    }
}
