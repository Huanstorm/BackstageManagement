using BackstageManagement.IRepository;
using BackstageManagement.IServices;
using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.Services
{
    public class RolePermissionServices : BaseServices<RolePermissionEntity>, IRolePermissionServices
    {
        readonly IRolePermissionRepository _dal;
        readonly IUnitOfWork _unitOfWork;
        public RolePermissionServices(IRolePermissionRepository dal,IUnitOfWork unitOfWork) {
            _dal = dal;
            base.BaseDal = dal;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<RolePermissionEntity>> QueryByRoleId(int roleId)
        {
            return await _dal.QueryByRoleId(roleId);
        }

        public async Task<int> SaveRolePermissions(int roleId, List<RolePermissionEntity> rolePermissions)
        {
            try
            {
                int count = 0;
                _unitOfWork.BeginTran();
                var deleteRes = await BaseDal.Delete(c => c.RoleId == roleId);
                foreach (var item in rolePermissions)
                {
                    RolePermissionEntity ep = new RolePermissionEntity();
                    ep.RoleId = roleId;
                    ep.PermissionId = item.PermissionId;
                    await _dal.Add(ep);
                    count++;
                }
                _unitOfWork.CommitTran();
                return count;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTran();
                throw ex;
            }
        }
    }
}
