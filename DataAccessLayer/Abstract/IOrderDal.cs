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
		public Order GetUserOrder(int userId);
		public IEnumerable<OrderItem> GetAllOrderItems(int id);
	}
}
