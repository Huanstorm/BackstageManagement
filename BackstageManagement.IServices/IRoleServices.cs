using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.IServices
{
    public interface IRoleServices:IBaseServices<RoleEntity>
    {
        Task<int> AddRole(RoleEntity entity);
    }
}
