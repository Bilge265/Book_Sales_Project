using EntityLayer.Concrete;
using EntityLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IBasketItemService : IGenericService<BasketItem>
    {

		public BasketItem TGetByBasketIdAndBookId(int basketId, int bookId);
		public void TRemoveItemFromBasket(int bookId);
		public void TUpdateItemQuantity(int basketId, int bookId, int newQuantity);
	}
}
