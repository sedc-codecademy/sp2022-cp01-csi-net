using CryptoSimulator.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.DataAccess.SQL
{
    public class Repository<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext : IDbContext
    {
        private IDbContext _context;
        private DbSet<TEntity> _dbSet;

        public Repository(IDbContext context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            TEntity entityTodelete = _dbSet.Find(id);
            Delete(entityTodelete);

        }


        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Deleted)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }


        public IEnumerable<TEntity> FilterBy(Func<TEntity, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }



    }
}
