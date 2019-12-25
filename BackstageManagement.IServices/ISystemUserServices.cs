using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.IServices
{
    public interface ISystemUserServices:IBaseServices<SystemUserEntity>
    {
        Task<SystemUserEntity> GetEmployeeByNo(string loginNo,string password);

        Task<int> AddEmployee(SystemUserEntity entity);
    }
}
