using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string Surname { get; set; }
        public string Name { get; set; }
		public string? Province { get; set; }

		public string? District { get; set; }

		public string? Street { get; set; }

		public string? AddressDesc { get; set; }
		public bool Status { get; set; }
        public byte[]? Image { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public List<Book> Books { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

	

	}
}
