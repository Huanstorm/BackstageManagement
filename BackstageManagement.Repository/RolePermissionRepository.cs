using BackstageManagement.IRepository;
using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.Repository
{
    public class RolePermissionRepository : BaseRepository<RolePermissionEntity>, IRolePermissionRepository
    {
        

        public RolePermissionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<RolePermissionEntity>> QueryByRoleId(int roleId)
        {
            var list =await Db.Queryable<RolePermissionEntity>()
                .Where(c => c.RoleId == roleId && !c.IsDeleted)
                .Mapper(it => it.Permission, it => it.PermissionId)
                .Mapper(it=>it.Role,it=>it.RoleId).ToListAsync();
            return list;
        }

    }
}
