using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Sales_Project.Controllers
{
	[AllowAnonymous]
	public class BookDetailController : Controller
	{
		private readonly IBookService _bookService;

		public BookDetailController(IBookService bookService)
		{
			_bookService = bookService;
		}

		[HttpGet]
		public IActionResult Index(int id)
		{
			var values = _bookService.TGetByID(id);
			return View(values);
		}
	}
}
