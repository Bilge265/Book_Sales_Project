using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using EntityLayer.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfBasketDal : GenericRepository<Basket>, IBasketDal
    {
        private readonly MyDbContext _context;

        public EfBasketDal(MyDbContext context) : base(context)
        {
			_context = context;
		}
		public IEnumerable<Basket> GetAllBasketItems()
		{
			return _context.Baskets.Include(b => b.BasketItems).ToList();
		}
	}
}
