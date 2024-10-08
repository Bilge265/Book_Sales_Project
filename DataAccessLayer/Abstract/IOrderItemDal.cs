﻿using EntityLayer.Concrete;
using EntityLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IOrderItemDal : IGenericDal<OrderItem>
    {
        public IEnumerable<OrderItem> GetAllOrderItems(int id);
        public IEnumerable<OrderItem> GetBookOrderItems(int orderId);
        public IEnumerable<OrderItem> GetOrderItems(int orderId);
        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId);
    }
}
