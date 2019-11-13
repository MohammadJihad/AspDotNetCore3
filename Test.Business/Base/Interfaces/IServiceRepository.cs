using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Business.Base.Interfaces
{
    public interface IServiceRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Create(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int key);
    }
}
