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
    public class RoleServices : BaseServices<RoleEntity>,IRoleServices
    {
        IRoleRepository _roleRepository;
        public RoleServices(IRoleRepository roleRepository) {
            _roleRepository = roleRepository;
            BaseDal = _roleRepository;
        }

        public async Task<int> AddRole(RoleEntity entity)
        {
            try
            {
                var role = await _roleRepository.GetSingle(c=>c.Name==entity.Name);
                if (role!=null)
                {
                    return -1;
                }
                return await _roleRepository.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
