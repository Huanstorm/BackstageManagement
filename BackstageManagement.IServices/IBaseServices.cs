using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.IServices
{
    public interface IBaseServices<TEntity> where TEntity:class
    {
        Task<TEntity> QueryById(object id);

        Task<List<TEntity>> Query(Expression<Func<TEntity,bool>> whereExpression);

        Task<List<TEntity>> GetAll();
        Task<TEntity> GetSingle(Expression<Func<TEntity,bool>> whereExpression);

        Task<int> Add(TEntity entity);

        Task<int> DeleteById(object id);

        Task<int> Delete(TEntity entity);

        Task<bool> Update(TEntity entity);
    }
}
