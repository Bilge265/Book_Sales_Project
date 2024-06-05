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
    public class EfOrderItemDal : GenericRepository<OrderItem>, IOrderItemDal
    {
        private readonly MyDbContext _context;

        public EfOrderItemDal(MyDbContext context) : base(context)
        {
            _context = context;

        }
        public IEnumerable<OrderItem> GetAllOrderItems(int id)
        {
            return _context.OrderItems
                .Include(x=>x.Orders ) 
                .Where(x=>x.Id == id).ToList();
                

           
        }
        public IEnumerable<OrderItem> GetBookOrderItems(int orderId)
        {
            return _context.OrderItems
                .Include(x => x.Orders) 
                .Where(x => x.OrderId == orderId) 
                .ToList();
        }
        public IEnumerable<OrderItem> GetOrderItems(int orderId)
        {
            return _context.OrderItems
                           .Include(oi => oi.Books)
                           .Where(oi => oi.OrderId == orderId)
                           .ToList();
        }
        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return _context.OrderItems
                           .Include(oi => oi.Books)
                           .Where(oi => oi.OrderId == orderId)
                           .ToList();
        }
    }
}
