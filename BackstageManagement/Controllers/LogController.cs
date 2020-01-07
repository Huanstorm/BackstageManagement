using BackstageManagement.IServices;
using BackstageManagement.Model;
using BackstageManagement.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BackstageManagement.Controllers
{
    public class LogController : BaseController
    {
        private readonly ISystemUserServices _employeeServices;
        public LogController(IRolePermissionServices rolePermissionServices,
            ILogServices logServices,
            ISystemUserServices employeeServices) : base(rolePermissionServices, logServices)
        {
            _employeeServices = employeeServices;
        }

        public async Task<ActionResult> Index()
        {
            await Task.Run(() => { }).ConfigureAwait(false);
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
        public async Task<ActionResult> GetLogs(int page, int limit, int? logtype, string daterange, string condition)
        {
            JsonResponse result = new JsonResponse();
            try
            {
                DateTime startDate = DateTime.Today;
                DateTime endDate = DateTime.Today.AddDays(1);
                if (!string.IsNullOrEmpty(daterange))
                {
                    startDate = Convert.ToDateTime(daterange.Split('~')[0].Trim());
                    endDate = Convert.ToDateTime(daterange.Split('~')[1].Trim()).AddDays(1);
                }
               
                var logs = await _logServices.QueryLogs(logtype,startDate,endDate,condition).ConfigureAwait(false);
                logs = logs.OrderByDescending(c => c.CreationTime).ToList();
                foreach (var item in logs)
                {
                    var user = await _employeeServices.QueryById(item.UserId).ConfigureAwait(false);
                    item.LoginName = user.LoginName;
                }
                result.data = logs.Skip((page - 1) * limit).Take(limit).ToList();
                result.count = logs.Count;
            }
            catch (Exception ex)
            {
                result.code = ResponseCode.Fail;
                result.msg = "查询日志失败，" + ex.ToString();
            }
            return Json(result);
        }
    }
}