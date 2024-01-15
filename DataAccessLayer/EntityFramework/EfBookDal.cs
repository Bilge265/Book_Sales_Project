using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
