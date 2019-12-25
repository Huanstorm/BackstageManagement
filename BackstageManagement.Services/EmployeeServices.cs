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
    public class EmployeeServices : BaseServices<EmployeeEntity>, IEmployeeServices
    {
        IEmployeeRepository _dal;
        public EmployeeServices(IEmployeeRepository dal) {
            this._dal = dal;
            base.BaseDal = dal;
        }


        public async Task<EmployeeEntity> GetEmployeeByNo(string loginNo,string password)
        {
            return await _dal.GetSingle(c => c.LoginNo == loginNo&&c.Password==password);
        }

        public async Task<int> AddEmployee(EmployeeEntity entity)
        {
            var user =await _dal.GetSingle(c=>c.LoginNo==entity.LoginNo&&c.IsDeleted==false);
            if (user!=null)
            {
                return -1;
            }
            return await _dal.Add(entity);
        }
    }
}
