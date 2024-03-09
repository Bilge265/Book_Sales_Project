using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
	public class EfBasketDal : GenericRepository<Basket>, IBasketDal
    {
        private readonly MyDbContext _context;

        public EfBasketDal(MyDbContext context) : base(context)
        {
			_context = context;
		}

		public IEnumerable<BasketItem> GetAllBasketItemsByBasketId(int id)
		{
			return _context.Baskets
				.Include(x => x.BasketItems)
					.ThenInclude(item => item.Books)
					.FirstOrDefault(basket => basket.Id == id)
					?.BasketItems;
		}

		public Basket GetUserBasket(int userId)
		{
			return _context.Baskets
				.Include(b => b.BasketItems)
				.FirstOrDefault(b => b.CustomerId == userId);
		}
	}
}
