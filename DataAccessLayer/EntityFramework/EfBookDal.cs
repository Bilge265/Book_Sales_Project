﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfBookDal: GenericRepository<Book>,IBookDal
    {
        private readonly MyDbContext _context;

        public EfBookDal(MyDbContext context) : base(context)
        {
            _context = context;
        }


		public List<Book> BookSearch(Expression<Func<Book, bool>> filter)
		{
			return _context.Set<Book>().Where(filter).ToList();
		}
		public Book GetBookById(int bookId)
		{
			return _context.Books.FirstOrDefault(b => b.ID == bookId);
		}
        public List<Book> GetBooksByUserId(int userId)
        {
            return _context.Books.Where(b => b.UserId == userId).ToList();
        }
    }
}
