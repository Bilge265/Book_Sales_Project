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
		public Order TGetUserOrder(int userId);
		public IEnumerable<OrderItem> TGetAllOrderItems(int id);
	}
}
