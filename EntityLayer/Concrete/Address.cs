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
    public class Address
	{
		[Key]
		public int ID {  get; set; }
		public string Province { get; set; } 

		public string District { get; set; } 

		public string Street { get; set; } 

		public string Description { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; }
		public virtual AppUser User { get; set; }
	}
}
