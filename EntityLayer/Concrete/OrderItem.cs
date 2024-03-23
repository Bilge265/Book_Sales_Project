using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class OrderItem
    {
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Books { get; set; }
    }
}
