using EntityLayer.Concrete;
using EntityLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderItemService : IGenericService<OrderItem>
    {
        public IEnumerable<OrderItem> TGetAllOrderItems(int id);
        public IEnumerable<OrderItem> TGetBookOrderItems(int orderId);
        public IEnumerable<OrderItem> TGetOrderItems(int orderId);
        public IEnumerable<OrderItem> TGetOrderItemsByOrderId(int orderId);
    }
}
