using EntityLayer.Concrete;
using EntityLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderService : IGenericService<Order>
    {
		IEnumerable<Order> TGetUserOrder(int userId);
        public IEnumerable<Order> TGetUserBookOrders(int userId);
        public List<Order> TUserSales(int userId);
        public Order TGetOrderById(int orderId);
        }
}
