using EntityLayer.Concrete;
using EntityLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IBasketService : IGenericService<Basket>
    {
		public Basket TGetUserBasket(int userId);
		public IEnumerable<BasketItem> TGetAllBasketItemsByBasketId(int id);

	}
}
