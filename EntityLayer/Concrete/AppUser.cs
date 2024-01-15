using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
    public class AppUser:  IdentityUser<int>
    {
        public string Surname { get; set; }
        public string? Address { get; set;}
        public string Name { get; set; }
        public bool Status { get; set; }
        public byte[]? Image { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public List<Book> Books { get; set; }

    }
}
