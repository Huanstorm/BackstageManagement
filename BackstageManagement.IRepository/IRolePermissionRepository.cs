using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.IRepository
{
    public interface IRolePermissionRepository:IBaseRepository<RolePermissionEntity>
    {
        Task<List<RolePermissionEntity>> QueryByRoleId(int roleId);

        
    }
}
