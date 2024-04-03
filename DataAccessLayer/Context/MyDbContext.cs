using EntityLayer.Concrete;
using EntityLayer.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class MyDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
       public MyDbContext(DbContextOptions options) : base(options){}

        public DbSet<Book>Books { get; set; }
		public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
		public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BasketItem>()
                .HasOne(bi => bi.Books)
                .WithMany()
                .HasForeignKey(bi => bi.BookId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
