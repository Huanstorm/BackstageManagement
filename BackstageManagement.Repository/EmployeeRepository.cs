using BackstageManagement.IRepository;
using BackstageManagement.Model.Models;


namespace BackstageManagement.Repository
{
    public class EmployeeRepository : BaseRepository<EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
