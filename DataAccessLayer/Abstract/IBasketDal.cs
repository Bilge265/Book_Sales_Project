using EntityLayer.Concrete;
using EntityLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
	public interface IBasketDal : IGenericDal<Basket>
	{

		public Basket GetUserBasket(int userId);
		public IEnumerable<BasketItem> GetAllBasketItemsByBasketId(int id);
	}
}
