using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
     public interface IBookService :IGenericService<Book>
    {
		List<Book> BookSearch(Expression<Func<Book, bool>> filter);
		public Book TGetBookById(int bookId);
	}
}
