using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_Sales_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IBookService _bookService;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(IBookService bookService, UserManager<AppUser> userManager)
        {
            _bookService = bookService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminPassword()
        {
            return ViewComponent("AdminPasswordView");
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            var isNewList = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Yeni Kitap" },
            new SelectListItem { Value = "false", Text = "İkinci El Kitap" },
        };
            ViewBag.isNewList = new SelectList(isNewList, "Value", "Text");
            return View("AddBook");
        }

        [HttpPost]
        public IActionResult AddBook(Book values, List<IFormFile> Image1, List<IFormFile> Image2, List<IFormFile> Image3)
        {
            var isNewList = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Yeni Kitap" },
            new SelectListItem { Value = "false", Text = "İkinci El Kitap" },
        };
            ViewBag.isNewList = new SelectList(isNewList, "Value", "Text");

            List<List<IFormFile>> imageLists = new List<List<IFormFile>> { Image1, Image2, Image3};
            List<byte[]> fileDataList = GetFileDataList(imageLists);
            values.ImageUrl1 = fileDataList.ElementAtOrDefault(0);
            values.ImageUrl2 = fileDataList.ElementAtOrDefault(1);
            values.ImageUrl3 = fileDataList.ElementAtOrDefault(2);

            BookValidator validator = new BookValidator();
            ValidationResult validationResult = validator.Validate(values);
            if (validationResult.IsValid)
            {  
                values.UserId = 1;
                values.CreationTime = DateTime.Now;
                _bookService.TAdd(values);
                TempData["SuccessMessage"] = "Kitap başarıyla eklendi!";
                return RedirectToAction("AddBook", "Admin");

            }
            else
            {
                TempData["ErrorMessage"] = "Kitap eklenemedi!";

                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
        [HttpGet]
        public IActionResult BookList()
        {
            var values = _bookService.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AdminBookDetail(int id)
        {
            var values = _bookService.TGetByID(id);
            return View(values);
        }

        [HttpGet]
        public IActionResult EditBook(int id)
        {
            var values =_bookService.TGetByID(id);
            var isNewList = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Yeni Kitap" },
            new SelectListItem { Value = "false", Text = "İkinci El Kitap" },
        };
            ViewBag.isNewList = new SelectList(isNewList, "Value", "Text");
            return View(values);
        }

        [HttpPost]
        public IActionResult EditBook(Book updateBook,int id, List<IFormFile> Image1, List<IFormFile> Image2, List<IFormFile> Image3)
        {
            var values = _bookService.TGetByID(id);

            var isNewList = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Yeni Kitap" },
            new SelectListItem { Value = "false", Text = "İkinci El Kitap" },
        };
            ViewBag.isNewList = new SelectList(isNewList, "Value", "Text");

            List<List<IFormFile>> imageLists = new List<List<IFormFile>> { Image1, Image2, Image3 };
            List<byte[]> fileDataList = GetFileDataList(imageLists);
            values.ImageUrl1 = fileDataList.ElementAtOrDefault(0);
            values.ImageUrl2 = fileDataList.ElementAtOrDefault(1);
            values.ImageUrl3 = fileDataList.ElementAtOrDefault(2);

            values.Author=updateBook.Author;
            values.Name = updateBook.Name;
            values.Description = updateBook.Description;
            values.Stock = updateBook.Stock;
            values.Price = updateBook.Price;
            values.PageCount = updateBook.PageCount;

            BookValidator validator = new BookValidator();
            ValidationResult validationResult = validator.Validate(values);
            if (validationResult.IsValid)
            {
                values.UserId = 1;
                values.UpdateTime = DateTime.Now;
                _bookService.TUpdate(values);          
                return RedirectToAction("BookList", "Admin");

            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }

        public IActionResult DeleteBook(int id)
        {
            var values =_bookService.TGetByID(id);
            _bookService.TDelete(values);
            return RedirectToAction("BookList", "Admin");
        }

        private List<byte[]> GetFileDataList(List<List<IFormFile>> imageLists)
        {
            List<byte[]> fileDataList = new List<byte[]>();
            foreach (var imageList in imageLists)
            {
                var file = imageList.FirstOrDefault(f => f.Length > 0);

                if (file != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileDataList.Add(ms.ToArray());
                    }
                }
            }
            return fileDataList;
        }

    }
}


