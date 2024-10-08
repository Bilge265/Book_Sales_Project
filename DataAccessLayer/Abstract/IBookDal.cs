﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IBookDal : IGenericDal<Book>
    {

		List<Book> BookSearch(Expression<Func<Book, bool>> filter);
		public Book GetBookById(int bookId);
        public List<Book> GetBooksByUserId(int userId);

    }
}
