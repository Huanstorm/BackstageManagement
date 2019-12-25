using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.IServices
{
    public interface IRolePermissionServices:IBaseServices<RolePermissionEntity>
    {
        Task<List<RolePermissionEntity>> QueryByRoleId(int roleId);
        Task<int> SaveRolePermissions(int roleId, List<RolePermissionEntity> rolePermissions);
    }
}
