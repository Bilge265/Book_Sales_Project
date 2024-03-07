using Book_Sales_Project.Models;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Book_Sales_Project.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;

		public HomeController(IBookService bookService)
		{
			_bookService = bookService;
		}

		public IActionResult Index(string p)
        {
            if (!string.IsNullOrEmpty(p))
            {
             return View(_bookService.BookSearch(x => x.Name.Contains(p)));
            }

            var bookList = _bookService.TGetList();
            return View(bookList);

		}
		public IActionResult NewBook(string p)
		{
			if (!string.IsNullOrEmpty(p))
			{
				return View(_bookService.BookSearch(x => x.Name.Contains(p)));
			}

			var bookList = _bookService.TGetList();
			return View(bookList);

		}
		public IActionResult SecondVerseBook(string p)
		{
			if (!string.IsNullOrEmpty(p))
			{
				return View(_bookService.BookSearch(x => x.Name.Contains(p)));
			}

			var bookList = _bookService.TGetList();
			return View(bookList);

		}


	}
}