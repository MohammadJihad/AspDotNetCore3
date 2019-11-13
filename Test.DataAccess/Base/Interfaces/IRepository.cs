using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataAccess.Base.Interfaces
{
    public interface IRepository<IEntity> where IEntity : class
    {
        Task<bool> Create(IEntity entity);
        Task<bool> Update(IEntity entity);
        Task<bool> Delete(IEntity entity);
        IQueryable<IEntity> GetAll();
        Task<IEntity> GetById(int Key);
    }
}
