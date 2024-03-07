using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Identity;

namespace EntityLayer.Concrete
{
    public class Basket
	{
		[Key]
		public int Id { get; set; }

        //[ForeignKey("Book")]
        //public int BookId { get; set; }
        //public virtual Book Book { get; set; }

        public virtual ICollection<BasketItem> BasketItems { get; set; }
        [ForeignKey("Customer")]
		public int CustomerId { get; set; }
		public virtual AppUser Customer { get; set; }
	}
}
