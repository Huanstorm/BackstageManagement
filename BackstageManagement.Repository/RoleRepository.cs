using BackstageManagement.IRepository;
using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.Repository
{
    public class RoleRepository : BaseRepository<RoleEntity>,IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
