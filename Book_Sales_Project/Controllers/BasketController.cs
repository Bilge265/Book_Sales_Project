using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Book_Sales_Project.Controllers
{
	[Authorize(Roles = "User")]
	public class BasketController : Controller
	{
		private readonly IBasketItemService _basketItemService;
	
		public BasketController(IBasketItemService basketItemService)
		{
			_basketItemService = basketItemService;
		}

		public IActionResult Index()
		{

			var basketItems = _basketItemService.GetAllBasketItems();
			return View(basketItems);


		}
	}
}
