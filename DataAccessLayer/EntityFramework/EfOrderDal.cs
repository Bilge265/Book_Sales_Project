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
		public Order GetUserOrder(int userId)
		{
			return _context.Orders
				.Include(b => b.OrderItems)
				.FirstOrDefault(b => b.CustomerId == userId);

		}
	
	}
}
