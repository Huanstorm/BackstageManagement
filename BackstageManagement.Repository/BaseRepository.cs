using BackstageManagement.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BackstageManagement.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly ISqlSugarClient _db;
        private readonly IUnitOfWork _unitOfWork;

        internal ISqlSugarClient Db {
            get {
                return _db;
            }
        }
        public BaseRepository(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
            _db = unitOfWork.GetDbClient();
        }
        public async Task<TEntity> QueryById(object id)
        {
            try
            {
                return await _db.Queryable<TEntity>().InSingleAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<TEntity>> QueryAll()
        {
            try
            {
                return await _db.Queryable<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                return await _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).SingleAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Add(TEntity entity)
        {
            try
            {
                var insert = _db.Insertable(entity);
                return await insert.ExecuteReturnIdentityAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteById(object id)
        {
            try
            {
                return await _db.Deleteable<TEntity>().In(id).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> Delete(TEntity entity)
        {
            try
            {
                return await _db.Deleteable<TEntity>().Where(entity).ExecuteCommandAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                return await _db.Deleteable<TEntity>(whereExpression).ExecuteCommandHasChangeAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Update(TEntity entity)
        {
            try
            {
                return await _db.Updateable(entity).ExecuteCommandHasChangeAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
