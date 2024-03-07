using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfOrderItemDal : GenericRepository<OrderItem>, IOrderItemDal
    {
        private readonly MyDbContext _context;

        public EfOrderItemDal(MyDbContext context) : base(context)
        {
        }
    }
}
