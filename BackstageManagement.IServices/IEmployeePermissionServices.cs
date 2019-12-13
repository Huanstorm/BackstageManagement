using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.IServices
{
    public interface IEmployeePermissionServices:IBaseServices<Employee_Permission>
    {
        Task<List<Employee_Permission>> QueryByEmployeeId(int employeeId);
        Task<int> SavePermissionInfo(int employeeId, List<Employee_Permission> employee_Permissions);
    }
}
