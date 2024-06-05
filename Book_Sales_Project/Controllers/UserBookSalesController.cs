using Book_Sales_Project.Models.OrderViewModels;
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
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderItemService _orderItemService;

        public UserBookSalesController(IBookService bookService, IOrderService orderService, UserManager<AppUser> userManager, IOrderItemService orderItemService)
        {
            _bookService = bookService;
            _orderService = orderService;
            _userManager = userManager;
            _orderItemService = orderItemService;
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
        [HttpGet]
        public async Task<IActionResult> BookList()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userBooks = _bookService.TGetBooksByUserId(user.Id);
            return View(userBooks);
        }
        [HttpGet]
        public IActionResult UserBookDetail(int id)
        {
            var values = _bookService.TGetByID(id);
            return View(values);
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

        public async Task<IActionResult> MyBookOrders()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userOrders = _orderService.TGetUserOrder(user.Id);
            var orderViewModels = new List<OrderViewModels>();

            foreach (var order in userOrders)
            {
                var orderItems = _orderItemService.TGetAllOrderItems(order.Id);
                var viewModel = new OrderViewModels
                {
                    OrderItems = orderItems,
                    Orders = order
                };
                orderViewModels.Add(viewModel);
            }

            return View(orderViewModels);

        }
        public async Task<IActionResult> MyUserOrders()
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

    }
}
