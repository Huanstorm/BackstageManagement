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
        readonly IRolePermissionRepository _rolePermissionRepository;
        readonly IUnitOfWork _unitOfWork;
        public RolePermissionServices(IRolePermissionRepository rolePermissionRepository,IUnitOfWork unitOfWork) {
            _rolePermissionRepository = rolePermissionRepository;
            base.BaseDal = rolePermissionRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<RolePermissionEntity>> QueryByRoleId(int roleId)
        {
            return await _rolePermissionRepository.QueryByRoleId(roleId);
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
                    ep.CreationTime = item.CreationTime;
                    await _rolePermissionRepository.Add(ep);
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
