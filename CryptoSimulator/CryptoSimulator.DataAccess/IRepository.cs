using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSimulator.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public void Delete(int id);
     


        public void Delete(TEntity entity);
       


        public IEnumerable<TEntity> FilterBy(Func<TEntity, bool> filter);
        


        public void Update(TEntity entity);
       

        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);
       
    }
}
