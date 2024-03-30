using Book_Sales_Project.Models.BasketViewModels;
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
        private readonly IBookService _bookService;
		private readonly IOrderItemService _orderItemService;
		private readonly IOrderService _orderService;
		private readonly UserManager<AppUser> _userManager;

		public OrderController(IBasketService basketService, IBasketItemService basketItemService, IBookService bookService, IOrderItemService orderItemService, IOrderService orderService, UserManager<AppUser> userManager)
		{
			_basketService = basketService;
			_basketItemService = basketItemService;
			_bookService = bookService;
			_orderItemService = orderItemService;
			_orderService = orderService;
			_userManager = userManager;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
        {
			var user = await _userManager.GetUserAsync(HttpContext.User);
			var userOrder = _basketService.TGetUserBasket(user.Id);
			var orderItems = _basketService.TGetAllBasketItemsByBasketId(userOrder.Id);

			var viewModel = new OrderViewModel
			{
				OrderItems = orderItems,
				Orders = userOrder,
			};
			return View(viewModel);			
        }

		[HttpPost]
		public IActionResult Index(int orderId)
		{
			return View();
		}
	}
}
