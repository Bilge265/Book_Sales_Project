using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class BasketItem
    {
        public int Id { get; set; }
		public int Quantity { get; set; }
		public decimal ProductTotalPrice { get; set; }

		[ForeignKey("Basket")]
        public int BasketId { get; set; }
		public virtual Basket Baskets { get; set; }

		[ForeignKey("Book")]
		public int BookId { get; set; }
		public virtual Book Books { get; set; }
	}
}
