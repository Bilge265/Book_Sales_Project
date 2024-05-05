using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class OrderItem
    {
        public int Id { get; set; }
		public int Quantity { get; set; }

        [ForeignKey("Order")]
        public int  OrderId { get; set; }
        public virtual Order Orders { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Books { get; set; }

	}
}
