using Book_Sales_Project.Models.BasketViewModels;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Sales_Project.Controllers
{
	[Authorize(Roles = "User")]
	public class BasketController : Controller
	{
		private readonly IBasketItemService _basketItemService;
		private readonly IBasketService _basketService;
		public BasketController(IBasketItemService basketItemService, IBasketService basketService)
		{
			_basketItemService = basketItemService;
			_basketService = basketService;
		}

		public IActionResult Index()
		{
			var basketItems = _basketItemService.TGetAllBasketItems();
			var baskets = _basketService.TGetByID(1);
			BasketViewModel model = new BasketViewModel
			{
				BasketItems = basketItems,
				Baskets = baskets
			};
			return View(model);
		}

	}
}
