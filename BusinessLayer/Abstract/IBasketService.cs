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
		public IEnumerable<Basket> GetAllBasketItems();
	}
}
