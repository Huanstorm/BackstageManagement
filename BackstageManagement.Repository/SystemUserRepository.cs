using BackstageManagement.IRepository;
using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackstageManagement.Repository
{
    public class SystemUserRepository : BaseRepository<SystemUserEntity>, ISystemUserRepository
    {
        public SystemUserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<List<SystemUserEntity>> QueryUsers(Expression<Func<SystemUserEntity, bool>> whereExpression)
        {
            var list = await Db.Queryable<SystemUserEntity>().WhereIF(whereExpression != null, whereExpression).Mapper(it => it.Role, it => it.RoleId).ToListAsync();
            return list;
        }
    }
}
