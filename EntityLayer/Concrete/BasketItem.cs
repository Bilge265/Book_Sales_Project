using EntityLayer.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class BasketItem
    {
        public int Id { get; set; }

		[ForeignKey("Book")]
		public int BookId { get; set; }
		public virtual Book Books { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }
	}
}
