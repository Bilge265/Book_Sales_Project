using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using EntityLayer.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
	public class EfBasketItemDal : GenericRepository<BasketItem>, IBasketItemDal
	{
		private readonly MyDbContext _context;

		public EfBasketItemDal(MyDbContext context) : base(context)
		{
			_context = context;
		}
		public BasketItem GetByBasketIdAndBookId(int basketId, int bookId)
		{
			return _context.BasketItems.FirstOrDefault(bi => bi.BasketId == basketId && bi.BookId == bookId);
		}
		public void RemoveItemFromBasket(int bookId)
		{
			var itemToRemove = _context.BasketItems.FirstOrDefault(i => i.BookId == bookId);

			if (itemToRemove != null)
			{
				var removedItemPrice = itemToRemove.ProductTotalPrice;
				_context.BasketItems.Remove(itemToRemove);


				var basket = _context.Baskets.FirstOrDefault();
				if (basket != null)
				{
					basket.TotalPrice -= removedItemPrice;
				}

				_context.SaveChanges();
			}
		}
		public void UpdateItemQuantity(int basketId, int bookId, int newQuantity)
		{
			var itemToUpdate = _context.BasketItems.FirstOrDefault(i => i.BasketId == basketId && i.BookId == bookId);

			if (itemToUpdate != null)
			{
				itemToUpdate.Quantity = newQuantity;
				_context.SaveChanges();
			}
		}


    }
}