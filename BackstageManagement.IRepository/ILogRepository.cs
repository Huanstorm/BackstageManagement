using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.IRepository
{
    public interface ILogRepository:IBaseRepository<LogEntity>
    {
        Task<List<LogEntity>> QueryLogs(int? logType, DateTime startDate, DateTime endDate, string condition);
    }
}
