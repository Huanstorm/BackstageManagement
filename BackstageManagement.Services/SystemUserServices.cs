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
    public class SystemUserServices : BaseServices<SystemUserEntity>, ISystemUserServices
    {
        ISystemUserRepository _dal;
        public SystemUserServices(ISystemUserRepository dal) {
            this._dal = dal;
            base.BaseDal = dal;
        }


        public async Task<SystemUserEntity> GetEmployeeByNo(string loginNo,string password)
        {
            return await _dal.GetSingle(c => c.LoginName == loginNo&&c.Password==password);
        }

        public async Task<int> AddEmployee(SystemUserEntity entity)
        {
            var user =await _dal.GetSingle(c=>c.LoginName==entity.LoginName&&c.IsDeleted==false);
            if (user!=null)
            {
                return -1;
            }
            return await _dal.Add(entity);
        }
    }
}
