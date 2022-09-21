using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.Common
{
    public  interface IDbContext
    {
        int SaveChanges();

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Entry(object entity);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void Dispose();
    }
}
