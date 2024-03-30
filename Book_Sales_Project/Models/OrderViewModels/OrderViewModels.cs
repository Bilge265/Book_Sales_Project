using EntityLayer.Concrete;

namespace Book_Sales_Project.Models.OrderViewModels
{
	public class OrderViewModels
	{
		public IEnumerable<OrderItem> OrderItems { get; set; }
		public Order Orders { get; set; }
	}
}
