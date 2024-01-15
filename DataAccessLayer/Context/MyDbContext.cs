using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
	public class MyDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
       public MyDbContext(DbContextOptions options) : base(options){}

       DbSet<Book>Books { get; set; }
    }
}
