using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityLayer.Identity;

namespace EntityLayer.Concrete
{
    public class Basket
	{
		[Key]
		public int Id { get; set; }
		public decimal TotalPrice { get; set; }

		public virtual ICollection<BasketItem> BasketItems { get; set; }

		[ForeignKey("Customer")]
		public int CustomerId { get; set; }
		public virtual AppUser Customer { get; set; }
	}
}
