using BackstageManagement.IRepository;
using BackstageManagement.Model.Models;


namespace BackstageManagement.Repository
{
    public class SystemUserRepository : BaseRepository<SystemUserEntity>, ISystemUserRepository
    {
        public SystemUserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
