using BackstageManagement.IRepository;
using BackstageManagement.IServices;
using BackstageManagement.Model;
using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.Services
{
    public class LogServices:BaseServices<LogEntity>, ILogServices
    {
        ILogRepository _logRepository;
        public LogServices(ILogRepository logRepository) {
            _logRepository = logRepository;
            BaseDal = logRepository;
        }
        public async Task WriteSystemLog(int loginId, string function, string content)
        {
            try
            {
                var response = await Common.CommonHelper.GetSouhuIpResponse();                     
                LogEntity entity = new LogEntity();
                entity.LogType = LogType.系统日志;
                entity.UserId = loginId;
                entity.CreationTime = DateTime.Now;
                entity.LogFunction = function;
                entity.LogContent = content;
                entity.Ip = response?.Ip + "";
                entity.CityName = (response != null ? response.Country + response.Region + response.City : null) + "";
                await _logRepository.Add(entity);
            }
            catch (Exception ex)
            {
                //忽略异常
            }
        }

        public async Task WriteExceptionLog(int loginId, string function, string content)
        {
            try
            {
                LogEntity entity = new LogEntity();
                entity.LogType = LogType.异常日志;
                entity.UserId = loginId;
                entity.CreationTime = DateTime.Now;
                entity.LogFunction = function;
                entity.LogContent = content;
                await _logRepository.Add(entity);
            }
            catch (Exception ex)
            {
                //忽略异常
            }
        }

        public async Task<List<LogEntity>> QueryLogs(int? logType, DateTime startDate, DateTime endDate, string condition)
        {
            try
            {
                return await _logRepository.QueryLogs(logType,startDate,endDate,condition);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
