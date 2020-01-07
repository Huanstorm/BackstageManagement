using BackstageManagement.IRepository;
using BackstageManagement.Model;
using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.Repository
{
    public class LogRepository : BaseRepository<LogEntity>, ILogRepository
    {
        public LogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<LogEntity>> QueryLogs(int? logType, DateTime startDate, DateTime endDate, string condition)
        {
            try
            {
                var logs =await Db.Queryable<LogEntity>().WhereIF(logType != null, c => c.LogType == (LogType)logType)
                    .WhereIF(startDate != null && endDate != null, c => c.CreationTime >= startDate && c.CreationTime <= endDate)
                    .WhereIF(!string.IsNullOrEmpty(condition), c => c.LogContent.Contains(condition) || c.LogFunction.Contains(condition)).ToListAsync();
                return logs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
