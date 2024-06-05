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
    public class OrderItemManager : IOrderItemService
    {
        IOrderItemDal _orderItemDal;

        public OrderItemManager(IOrderItemDal orderItemDal)
        {
            _orderItemDal = orderItemDal;
        }
        public IEnumerable<OrderItem> TGetAllOrderItems(int id)
        {
            return _orderItemDal.GetAllOrderItems(id);
        }
        public void TAdd(OrderItem t)
        {
            _orderItemDal.Insert(t);
        }

        public void TDelete(OrderItem t)
        {
            _orderItemDal.Delete(t);
        }

        public OrderItem TGetByID(int id)
        {
           return _orderItemDal.GetById(id);
        }

        public List<OrderItem> TGetList()
        {
           return _orderItemDal.GetList();
        }

        public void TUpdate(OrderItem t)
        {
            _orderItemDal.Update(t);
        }

        public IEnumerable<OrderItem> TGetBookOrderItems(int orderId)
        {
          return  _orderItemDal.GetBookOrderItems(orderId);
        }

        public IEnumerable<OrderItem> TGetOrderItems(int orderId)
        {
           return _orderItemDal.GetOrderItems(orderId);
        }
        public IEnumerable<OrderItem> TGetOrderItemsByOrderId(int orderId)
        {
            return _orderItemDal.GetOrderItemsByOrderId(orderId);
        }
    }
}
