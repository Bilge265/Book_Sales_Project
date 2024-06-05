using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfOrderDal : GenericRepository<Order>, IOrderDal
    {
        private readonly MyDbContext _context;

        public EfOrderDal(MyDbContext context) : base(context)
        {
			_context = context;

		}
		public IEnumerable<Order> GetUserOrder(int userId)
		{
			return _context.Orders
				.Include(b => b.OrderItems)
				.Where(b => b.CustomerId == userId)
				.ToList();
		}
        public IEnumerable<Order> GetUserBookOrders(int userId)
        {
            var userBookIds = _context.Books.Where(b => b.UserId == userId).Select(b => b.ID).ToList();
            var userOrders = _context.Orders
                                     .Include(o => o.OrderItems)
                                     .ThenInclude(oi => oi.Books) 
                                     .Include(o => o.Customer)
                                     .Where(o => o.OrderItems.Any(oi => userBookIds.Contains(oi.BookId)))
                                     .ToList();

            return userOrders;
        }
        public List<Order> UserSales(int userId)
        {
            return _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Books)
                    .Include(o => o.Customer)
                    .Where(o => o.OrderItems.Any(oi => oi.Books.UserId != userId))
                    .ToList();
        }
        public Order GetOrderById(int orderId)
        {
            return _context.Orders
        .Include(o => o.Customer)
        .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Books)
        .FirstOrDefault(o => o.Id == orderId);
        }
    }
}
