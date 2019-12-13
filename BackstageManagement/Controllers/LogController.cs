using BackstageManagement.IServices;
using BackstageManagement.Model;
using BackstageManagement.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BackstageManagement.Controllers
{
    public class LogController : BaseController
    {
        private readonly IEmployeeServices _employeeServices;
        public LogController(IEmployeePermissionServices employeePermissionServices,
            ILogServices logServices,
            IEmployeeServices employeeServices) : base(employeePermissionServices,logServices)
        {
            _employeeServices = employeeServices;
        }

        public async Task<ActionResult> Index()
        {
            await Task.Run(() => { });
            return View();
        }
        /// <summary>
        /// 分页获取日志信息
        /// </summary>
        /// <param name="page">页</param>
        /// <param name="limit">显示行数</param>
        /// <param name="system">所属系统</param>
        /// <param name="logtype">日志类型</param>
        /// <param name="daterange">日期范围</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public async Task<ActionResult> GetLogInfo(int page, int limit, int? system, int? logtype, string daterange, string condition)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                List<LogEntity> logs =await _logServices.GetAll();
                if (system != null)
                {
                    logs = logs.Where(c => c.BelongSystem == (BelongSystem)system).ToList();
                }
                if (logtype != null)
                {
                    logs = logs.Where(c => c.LogType == (LogType)logtype).ToList();
                }
                if (!string.IsNullOrEmpty(daterange))
                {
                    var startDate = Convert.ToDateTime(daterange.Split('~')[0].Trim());
                    var endDate = Convert.ToDateTime(daterange.Split('~')[1].Trim()).AddDays(1);
                    logs = logs.Where(c => c.CreationTime >= startDate && c.CreationTime <= endDate).ToList();
                }
                if (!string.IsNullOrEmpty(condition))
                {
                    logs = logs.Where(c => c.LogFunction.Contains(condition) || c.LogContent.Contains(condition)).ToList();
                }
                logs = logs.OrderByDescending(c => c.CreationTime).ToList();
                foreach (var item in logs)
                {
                    var user =await _employeeServices.QueryById(item.LoginId);
                    item.LoginNo = user.LoginNo;
                }
                result.data = logs.Skip((page - 1) * limit).Take(limit).ToList();
                result.count = logs.Count;
            }
            catch (Exception ex)
            {
                result.code = ResponseCode.Fail;
                result.msg = "查询日志信息失败，" + ex.ToString();
            }
            return Json(result);
        }
    }
}