using BackstageManagement.IRepository;
using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.Repository
{
    public class EmployeePermissionRepository : BaseRepository<Employee_Permission>, IEmployeePermissionRepository
    {
        

        public EmployeePermissionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<Employee_Permission>> QueryByLoginId(int employeeId)
        {
            var list =await Db.Queryable<Employee_Permission>().Mapper(it => it.Permission, it => it.PermissionId).ToListAsync();
            return list.Where(c=>c.EmployeeId== employeeId).ToList();
        }

    }
}
