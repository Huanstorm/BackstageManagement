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
    public class PermissionServices:BaseServices<PermissionEntity>,IPermissionServices
    {
        IPermissionRepository _dal;
        public PermissionServices(IPermissionRepository dal) {
            _dal = dal;
            BaseDal = dal;
        }
    }
}
