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
    public class BasketManager : IBasketService
    {
        IBasketDal _basketDal;

        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }

		public IEnumerable<Basket> GetAllBasketItems()
		{
		return _basketDal.GetAllBasketItems();
		}

		public void TAdd(Basket t)
        {
            _basketDal.Insert(t);
        }

        public void TDelete(Basket t)
        {
            _basketDal.Delete(t);
        }

        public Basket TGetByID(int id)
        {
            return _basketDal.GetById(id);
        }

        public List<Basket> TGetList()
        {
           return _basketDal.GetList();
        }

        public void TUpdate(Basket t)
        {
            _basketDal.Update(t);
        }
    }
}
