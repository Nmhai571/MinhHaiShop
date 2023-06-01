using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhHaiShop.Data.Infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private MinhHaiShopDbContext _context;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public MinhHaiShopDbContext DbContext
        {
            get { return _context ?? (_context = _dbFactory.Init()); }
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
