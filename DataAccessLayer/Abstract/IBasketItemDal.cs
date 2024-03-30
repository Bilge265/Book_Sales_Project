using EntityLayer.Concrete;
using EntityLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IBasketItemDal : IGenericDal<BasketItem>
    {
		public BasketItem GetByBasketIdAndBookId(int basketId, int bookId);
		public void RemoveItemFromBasket(int bookId);
		public void UpdateItemQuantity(int basketId, int bookId, int newQuantity);
	}
}
