using BackstageManagement.IRepository;
using BackstageManagement.IServices;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackstageManagement.Services
{
    public class BaseServices<TEntity>:IBaseServices<TEntity> where TEntity:class,new()
    {
        public IBaseRepository<TEntity> BaseDal;

        public async Task<TEntity> QueryById(object id)
        {
            return await BaseDal.QueryById(id);
        }
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseDal.Query(whereExpression);
        }
        public async Task<int> Add(TEntity entity)
        {
            return await BaseDal.Add(entity);
        }
        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseDal.GetSingle(whereExpression);
        }
        public async Task<int> Delete(TEntity entity)
        {
            return await BaseDal.Delete(entity);
        }

        public async Task<int> DeleteById(object id)
        {
            return await BaseDal.DeleteById(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await BaseDal.QueryAll();
        }

        public async Task<bool> Update(TEntity entity)
        {
            return await BaseDal.Update(entity);
        }

        
    }
}
