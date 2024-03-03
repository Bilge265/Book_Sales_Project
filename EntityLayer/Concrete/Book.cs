using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
	public class Book 
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
		public string Publisher { get; set; }
		public bool Status { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string? Type { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }
        public int PageCount { get; set; }
        public bool? isNew { get; set; }

        //[ForeignKey("User")]
        //public int UserId { get; set; }
        //public virtual AppUser User { get; set; }
        //public virtual ICollection<BookSale> BookSales { get; set; }
        //public virtual ICollection<BookImage> BookImages { get; set; }
        public virtual BookSeller BookSellers { get; set; }
        public byte[]? ImageUrl1 { get; set; }
        public byte[]? ImageUrl2 { get; set; }

    }
}
