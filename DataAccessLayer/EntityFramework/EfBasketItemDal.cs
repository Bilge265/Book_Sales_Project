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
    public class EfBasketItemDal : GenericRepository<BasketItem>, IBasketItemDal
    {
        private readonly MyDbContext _context;

        public EfBasketItemDal(MyDbContext context) : base(context)
        {
			_context = context;
		}

    }
}
