using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.IRepository
{
    public interface ISystemUserRepository:IBaseRepository<SystemUserEntity>
    {
        Task<List<SystemUserEntity>> QueryUsers(Expression<Func<SystemUserEntity, bool>> whereExpression);
    }
}
