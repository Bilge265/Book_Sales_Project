using Book_Sales_Project.Models;
using Book_Sales_Project.Models.OrderViewModels;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using EntityLayer.Identity;
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
        private readonly IUserService _userService;
        private readonly IOrderItemService _orderItemService;
        private readonly IOrderService _orderService;

        public AdminController(IBookService bookService, UserManager<AppUser> userManager, IUserService userService, IOrderItemService orderItemService, IOrderService orderService)
        {
            _bookService = bookService;
            _userManager = userManager;
            _userService = userService;
            _orderItemService = orderItemService;
            _orderService = orderService;
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
            var userId = _userManager.GetUserId(HttpContext.User);
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

            BookValidator validator = new BookValidator();
            ValidationResult validationResult = validator.Validate(values);
            if (validationResult.IsValid)
            {
                values.Status = true;
                values.UserId = int.Parse(userId);
                values.CreationTime = DateTime.Now;
                _bookService.TAdd(values);
		
				return RedirectToAction("AddBook", "Admin");

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

		[HttpGet]
		public IActionResult AdminPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AdminPassword(PasswordViewModel model)
		{
			if (ModelState.IsValid)
			{

				var user = await _userManager.GetUserAsync(HttpContext.User);

				var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
				if (result.Succeeded)
				{

					TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirildi.";
					return RedirectToAction("AdminPassword", "Admin");
				}
				else
				{

					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return View(model);
				}
			}
			else
			{
				return View(model);
			}
		}
        [HttpGet]
        public IActionResult UserList()
        {
            var values = _userService.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult SalesRequest()
        {
            var values = _bookService.TGetList();
            return View(values);
        }
        public IActionResult StatusBook(int id)
        {
            var values = _bookService.TGetByID(id);
            values.Status = !values.Status;
            _bookService.TUpdate(values);
            return RedirectToAction("SalesRequest", "Admin");
        }

        
        public async Task<IActionResult> GetOrder()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userBookOrders = _orderService.TGetUserBookOrders(user.Id);
            var orderViewModels = new List<OrderViewModels>();

            foreach (var order in userBookOrders)
            {
                var orderItems = _orderItemService.TGetBookOrderItems(order.Id);
                var viewModel = new OrderViewModels
                {
                    OrderItems = orderItems,
                    Orders = order
                };
                orderViewModels.Add(viewModel);
            }

            return View(orderViewModels);
        }
        [HttpPost]
        public async Task<IActionResult> OrderDetail(int orderId)
        {
            var order = _orderService.TGetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var orderItems = _orderItemService.TGetOrderItemsByOrderId(orderId).ToList();
            var orderDetailViewModel = new OrderViewModels
            {
                Orders = order,
                OrderItems = orderItems
            };

            return View(orderDetailViewModel);
        }
        public async Task<IActionResult> UserSalesOrder()
        {       
            int adminUserId = 1;
            var userBookOrders = _orderService.TUserSales(adminUserId);
            var orderViewModels = new List<OrderViewModels>();

            foreach (var order in userBookOrders)
            {
                var orderItems = _orderItemService.TGetOrderItems(order.Id);
                var viewModel = new OrderViewModels
                {
                    OrderItems = orderItems,
                    Orders = order
                };
                orderViewModels.Add(viewModel);
            }

            return View(orderViewModels);
        }
        [HttpPost]
        public async Task<IActionResult> UserOrderDetail(int orderId)
        {
            var order = _orderService.TGetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var orderItems = _orderItemService.TGetOrderItemsByOrderId(orderId).ToList();
            var orderDetailViewModel = new OrderViewModels
            {
                Orders = order,
                OrderItems = orderItems
            };

            return View(orderDetailViewModel);
        }
    }
}


