using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using EntityLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfAddressDal : GenericRepository<Address>, IAddressDal
    {
        private readonly MyDbContext _context;

        public EfAddressDal(MyDbContext context) : base(context)
        {
        }
    }
}
