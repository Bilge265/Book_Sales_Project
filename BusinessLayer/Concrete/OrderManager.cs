using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void TAdd(Order t)
        {
            _orderDal.Insert(t);
        }

        public void TDelete(Order t)
        {
            _orderDal.Delete(t);
        }
        public Order TGetOrderById(int orderId)
        {
            return _orderDal.GetOrderById(orderId);
        }


        public Order TGetByID(int id)
        {
            return _orderDal.GetById(id);
        }

        public List<Order> TGetList()
        {
            return _orderDal.GetList();
        }

        public IEnumerable<Order> TGetUserBookOrders(int userId)
        {
            return _orderDal.GetUserBookOrders(userId);
        }

        public IEnumerable<Order> TGetUserOrder(int userId)
		{
			return _orderDal.GetUserOrder(userId);
		}

		public void TUpdate(Order t)
        {
            _orderDal.Update(t);
        }

        public List<Order> TUserSales(int userId)
        {
            return _orderDal.UserSales(userId);
        }
    }
}
