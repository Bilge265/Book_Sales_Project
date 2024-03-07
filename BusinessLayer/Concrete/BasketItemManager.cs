using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class BasketItemManager : IBasketItemService
    {
        IBasketItemDal _basketItemDal;

        public BasketItemManager(IBasketItemDal basketItemDal)
        {
            _basketItemDal = basketItemDal;
        }

		public IEnumerable<BasketItem> GetAllBasketItems()
		{
			return _basketItemDal.GetAllBasketItems();
			
		}

		public void TAdd(BasketItem t)
        {
            _basketItemDal.Insert(t);
        }

        public void TDelete(BasketItem t)
        {
            _basketItemDal.Delete(t);
        }

        public BasketItem TGetByID(int id)
        {
           return _basketItemDal.GetById(id);
        }

        public List<BasketItem> TGetList()
        {
           return _basketItemDal.GetList();
        }

        public void TUpdate(BasketItem t)
        {
           _basketItemDal.Update(t);
        }
    }
}
