using Microsoft.AspNetCore.Mvc;

namespace Book_Sales_Project.Controllers
{
	public class RecommendationViewController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
