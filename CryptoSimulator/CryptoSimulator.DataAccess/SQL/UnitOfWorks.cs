using CryptoSimulator.Common;
using CryptoSimulator.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.DataAccess.SQL
{
    public class UnitOfWorks<TContext> : IDisposable, IUnitOfWorks<TContext> where TContext : class, IDbContext, new()
    {
        private IDbContext _context;
        public UnitOfWorks()
        {
            _context = new TContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<T> GetGenericRepository<T>() where T : class
        {
            return new Repository<T, TContext>(_context);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T, TContext>(_context);
        }

        public IDbContext ReturnContext()
        {
            return _context;
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
