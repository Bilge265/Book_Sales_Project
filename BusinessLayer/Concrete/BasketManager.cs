using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
	public class BasketManager : IBasketService
    {
        IBasketDal _basketDal;

        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
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

		public Basket TGetUserBasket(int userId)
		{
			return _basketDal.GetUserBasket(userId);
		}

		public IEnumerable<BasketItem> TGetAllBasketItemsByBasketId(int id)
		{
			return _basketDal.GetAllBasketItemsByBasketId(id);
		}
	}
}
