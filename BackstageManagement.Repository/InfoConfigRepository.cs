using BackstageManagement.IRepository;
using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.Repository
{
    public class InfoConfigRepository : BaseRepository<InfoConfigEntity>, IInfoConfigRepository
    {
        public InfoConfigRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
