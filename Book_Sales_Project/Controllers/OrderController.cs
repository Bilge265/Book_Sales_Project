
using Book_Sales_Project.Models.BasketViewModels;
using Book_Sales_Project.Models.OrderViewModels;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book_Sales_Project.Controllers
{
    [Authorize(Roles = "User")]
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IBasketItemService _basketItemService;
		private readonly IOrderItemService _orderItemService;
		private readonly IOrderService _orderService;
		private readonly UserManager<AppUser> _userManager;
		public OrderController(IBasketService basketService, IBasketItemService basketItemService,  IOrderItemService orderItemService, IOrderService orderService, UserManager<AppUser> userManager)
		{
			_basketService = basketService;
			_basketItemService = basketItemService;
			_orderItemService = orderItemService;
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
		public async Task<IActionResult> Index(BasketViewModel viewModel)
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			var userBasket = _basketService.TGetUserBasket(user.Id);
			var basketItems = _basketService.TGetAllBasketItemsByBasketId(userBasket.Id);
			decimal totalPrice = userBasket.TotalPrice;		
			var order = new Order
			{				
				CustomerId = user.Id,
				OrderDate = DateTime.Now,
				TotalPrice = totalPrice,
				Province = viewModel.Baskets.Customer.Province,
				District = viewModel.Baskets.Customer.District,
				Street = viewModel.Baskets.Customer.Street,
				AddressDesc = viewModel.Baskets.Customer.AddressDesc
			};

			_orderService.TAdd(order);


			foreach (var basketItem in userBasket.BasketItems)
			{
				var orderItem = new OrderItem
				{
					OrderId= order.Id,
					Quantity = basketItem.Quantity,
					BookId = basketItem.BookId,
				};

				_orderItemService.TAdd(orderItem);
			}

			if (basketItems != null)
			{

				foreach (var basketItem in basketItems)
				{
					_basketItemService.TDelete(basketItem);
				}
				var updatedBasketItems = _basketService.TGetAllBasketItemsByBasketId(userBasket.Id);
				decimal totalPrice2 = updatedBasketItems.Sum(item => item.ProductTotalPrice);

				userBasket.TotalPrice = totalPrice2;
				_basketService.TUpdate(userBasket);
				return RedirectToAction("MyOrders", "User");
			}
			else
			{
				TempData["ErrorMessage"] = "Kullanıcıya ait bir sepet bulunamadı.";
				return RedirectToAction("Index", "Basket");
			}
		
		}
	}
}
