using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Book_Sales_Project.ViewComponents.Admin
{
    public class AdminPasswordView: ViewComponent
    {    

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
