using EntityLayer.Concrete;
using EntityLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IOrderDal : IGenericDal<Order>
    {
		IEnumerable<Order> GetUserOrder(int userId);
        public IEnumerable<Order> GetUserBookOrders(int userId);
        public List<Order> UserSales(int userId);
        public Order GetOrderById(int orderId);
        }
}
