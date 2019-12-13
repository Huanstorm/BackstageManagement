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
    public class EmployeePermissionServices : BaseServices<Employee_Permission>, IEmployeePermissionServices
    {
        IEmployeePermissionRepository _dal;
        IUnitOfWork _unitOfWork;
        public EmployeePermissionServices(IEmployeePermissionRepository dal,IUnitOfWork unitOfWork) {
            _dal = dal;
            base.BaseDal = dal;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Employee_Permission>> QueryByEmployeeId(int employeeId)
        {
            return await _dal.QueryByLoginId(employeeId);
        }

        public async Task<int> SavePermissionInfo(int employeeId, List<Employee_Permission> employee_Permissions)
        {
            try
            {
                int count = 0;
                _unitOfWork.BeginTran();
                var deleteRes = await BaseDal.Delete(c => c.EmployeeId == employeeId);
                foreach (var item in employee_Permissions)
                {
                    Employee_Permission ep = new Employee_Permission();
                    ep.EmployeeId = employeeId;
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
