using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.IServices
{
    public interface IEmployeeServices:IBaseServices<EmployeeEntity>
    {
        Task<EmployeeEntity> GetEmployeeByNo(string loginNo,string password);

        Task<int> AddEmployee(EmployeeEntity entity);
    }
}
