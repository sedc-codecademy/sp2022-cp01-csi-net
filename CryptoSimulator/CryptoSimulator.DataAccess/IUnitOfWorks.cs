using CryptoSimulator.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.DataAccess
{
    public interface IUnitOfWorks<TContext> where TContext : IDbContext, new()
    {
        IRepository<T> GetGenericRepository<T>()
          where T : class;
        void SaveChanges();
        public void Dispose();
        IDbContext ReturnContext();
    }
}
