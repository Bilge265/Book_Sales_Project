using EntityLayer.Concrete;
using System.ComponentModel.DataAnnotations;

namespace Book_Sales_Project.Models.BasketViewModels
{
	public class BasketViewModel
	{
		public IEnumerable<BasketItem> BasketItems { get; set; }
		public Basket Baskets { get; set; }

	}
}
