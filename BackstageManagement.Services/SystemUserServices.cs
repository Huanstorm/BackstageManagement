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
        ISystemUserRepository _systemUserServices;
        public SystemUserServices(ISystemUserRepository dal) {
            this._systemUserServices = dal;
            base.BaseDal = dal;
        }


        public async Task<SystemUserEntity> GetUserByLoginName(string loginNo,string password)
        {
            return await _systemUserServices.GetSingle(c => c.LoginName == loginNo&&c.Password==password&&!c.IsDeleted);
        }

        public async Task<int> AddEmployee(SystemUserEntity entity)
        {
            var user =await _systemUserServices.GetSingle(c=>c.LoginName==entity.LoginName&&!c.IsDeleted);
            if (user!=null)
            {
                return -1;
            }
            return await _systemUserServices.Add(entity);
        }

        public async Task<List<SystemUserEntity>> QueryUsers()
        {
            var list =await _systemUserServices.QueryUsers(c=>!c.IsDeleted);
            return list;
        }
    }
}
