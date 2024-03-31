using EntityLayer.Concrete;

namespace Book_Sales_Project.Models.BasketViewModels
{
	public class BasketViewModel
	{
		public IEnumerable<BasketItem> BasketItems { get; set; }
		public Basket Baskets { get; set; }
		public IEnumerable<Address> Address { get; set; }
	}
}
