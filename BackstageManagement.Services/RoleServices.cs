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
    }
}
