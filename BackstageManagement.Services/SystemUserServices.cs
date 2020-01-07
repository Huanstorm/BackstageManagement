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
        ISystemUserRepository _systemUserRepository;
        public SystemUserServices(ISystemUserRepository dal) {
            this._systemUserRepository = dal;
            base.BaseDal = dal;
        }


        public async Task<SystemUserEntity> GetUserByLoginName(string loginNo,string password)
        {
            return await _systemUserRepository.GetSingle(c => c.LoginName == loginNo&&c.Password==password&&!c.IsDeleted);
        }

        public async Task<int> AddEmployee(SystemUserEntity entity)
        {
            var user =await _systemUserRepository.GetSingle(c=>c.LoginName==entity.LoginName&&!c.IsDeleted);
            if (user!=null)
            {
                return -1;
            }
            return await _systemUserRepository.Add(entity);
        }

        public async Task<List<SystemUserEntity>> QueryUsers()
        {
            var list =await _systemUserRepository.QueryUsers(c=>!c.IsDeleted);
            return list;
        }
    }
}
