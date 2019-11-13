using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataAccess.Base.Interfaces;
using Test.DataAccess.DBContext;

namespace Test.DataAccess.Base
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        private readonly TestDbContext _dbContext;
        public Repository(TestDbContext testDbContext)
        {
            _dbContext = testDbContext;
        }
        public async Task<bool> Create(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        public async Task<TEntity> GetById(int Key)
        {
            return await _dbContext.Set<TEntity>().FindAsync(Key);
        }
        public async Task<bool> Update(TEntity entity)
        {
            _dbContext.Update(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
