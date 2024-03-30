﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityLayer.Identity;

namespace EntityLayer.Concrete
{
    public class Order
	{
		[Key]
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalPrice { get; set; }
	
		public virtual ICollection<OrderItem> OrderItems { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual AppUser Customer { get; set; }


    }
}
