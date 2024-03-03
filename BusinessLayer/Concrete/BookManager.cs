using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class BookManager : IBookService
    {
        IBookDal _bookDal;

        public BookManager(IBookDal bookDal)
        {
            _bookDal = bookDal;
        }

		public List<Book> BookSearch(Expression<Func<Book, bool>> filter)
		{
		 return	_bookDal.BookSearch(filter);
		}

		public void TAdd(Book t)
        {
           _bookDal.Insert(t);
        }

        public void TDelete(Book t)
        {
            _bookDal.Delete(t);
        }

        public Book TGetByID(int id)
        {
            return _bookDal.GetById(id);
        }

        public List<Book> TGetList()
        {
            return _bookDal.GetList();
        }

        public void TUpdate(Book t)
        {
            _bookDal.Update(t);
        }
    }
}
