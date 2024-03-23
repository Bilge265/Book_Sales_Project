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

		//[HttpPost]
		//public async Task<IActionResult> AddToBasket(BasketItem basketItem)
		//{
		//	var basketItems = _basketItemService.TGetList();
		//	var baskets = _basketService.TGetByID(1);
		//	return View();

	//}
	[HttpPost]
	public async Task<IActionResult> AddToBasket(int bookId, int quantity)
	{
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var userBasket = _basketService.TGetUserBasket(user.Id); // Bu metod kullanıcının sepetini getirir, eğer yoksa null döner

            // Eğer kullanıcının sepeti yoksa, yeni bir sepet oluştur
            if (userBasket == null)
            {
				var userBasketNew = new Basket();
				userBasketNew.TotalPrice = 0;
				userBasketNew.CustomerId = user.Id;
				_basketService.TAdd(userBasketNew);

                /*userBasket = _basketService.TAdd();*/ // Yeni bir sepet oluşturur ve kullanıcının sepetini döner
            }

            // Şimdi basketId değerini kullanıcının sepetinin Id'sine eşitliyoruz


            // Yeni bir BasketItem nesnesi oluşturuluyor ve gerekli alanlar atanıyor
            var basketItem = new BasketItem
            {
                BookId = bookId,
                Quantity = quantity,
                BasketId = userBasket.Id // BasketId değeri atanıyor
            };

            // BasketItemService üzerinden sepete öğe ekleniyor
            _basketItemService.TAdd(basketItem);

            // Kullanıcıyı anasayfaya yönlendiriyoruz
            return RedirectToAction("Index", "Home");
        }
}
}
