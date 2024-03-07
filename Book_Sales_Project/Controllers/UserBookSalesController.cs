using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using EntityLayer.Identity;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_Sales_Project.Controllers
{
    public class UserBookSalesController : Controller
    {
        private readonly IBookService _bookService;
        private readonly UserManager<AppUser> _userManager;

        public UserBookSalesController(IBookService bookService, UserManager<AppUser> userManager)
        {
            _bookService = bookService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddBook()
        {        
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(Book values, List<IFormFile> Image1, List<IFormFile> Image2, List<IFormFile> Image3)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            List<List<IFormFile>> imageLists = new List<List<IFormFile>> { Image1, Image2, Image3 };
            List<byte[]> fileDataList = GetFileDataList(imageLists);
            values.ImageUrl1 = fileDataList.ElementAtOrDefault(0);
            values.ImageUrl2 = fileDataList.ElementAtOrDefault(1);

            BookValidator validator = new BookValidator();
            ValidationResult validationResult = validator.Validate(values);
            
            if (validationResult.IsValid)
            {                
                values.Status = false;
                values.UserId = int.Parse(userId);
                values.isNew = false;
                values.CreationTime = DateTime.Now;
                _bookService.TAdd(values);
                return RedirectToAction("AddBook", "UserBookSales");

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
