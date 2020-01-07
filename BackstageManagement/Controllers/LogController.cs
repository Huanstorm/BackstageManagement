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
                //Expression<Func<LogEntity, bool>> pre = Common.ExpressionHelper.True<LogEntity>();
                //if (logtype != null)
                //{
                //    pre = pre.And(s => s.LogType == (LogType)logtype);
                //}
                //if (!string.IsNullOrEmpty(daterange))
                //{
                //    var startDate = Convert.ToDateTime(daterange.Split('~')[0].Trim());
                //    var endDate = Convert.ToDateTime(daterange.Split('~')[1].Trim()).AddDays(1);
                //    //pre = pre.And(c => c.CreationTime >= "" && c.CreationTime <= endDate);
                //}
                //if (!string.IsNullOrEmpty(condition))
                //{
                //    pre = pre.And(c => c.LogFunction.Contains(condition) || c.LogContent.Contains(condition));
                //}
                //var startDate = Convert.ToDateTime(daterange.Split('~')[0].Trim());
                //var endDate = Convert.ToDateTime(daterange.Split('~')[1].Trim()).AddDays(1);
                //var logs = await _logServices.Query(null);
                //logs = logs.AsQueryable().WhereIF(logtype != null, c => c.LogType == (LogType)logtype)
                //    .Where(c => c.CreationTime >= startDate && c.CreationTime <= endDate)
                //    .WhereIF(!string.IsNullOrEmpty(condition), c => c.LogFunction.Contains(condition) || c.LogContent.Contains(condition) || c.LoginName.Contains(condition))
                //    .OrderByDescending(c => c.CreationTime).ToList();
                //foreach (var item in logs)
                //{
                //    var user =await _employeeServices.QueryById(item.UserId);
                //    item.LoginName = user?.LoginName;
                //}
                var expressionable = SqlSugar.Expressionable.Create<LogEntity>();
                expressionable.AndIF(logtype != null, c => c.LogType == (LogType)logtype)
                    .AndIF(!string.IsNullOrEmpty(daterange), c => c.CreationTime >= Convert.ToDateTime(daterange.Split('~')[0].Trim()) && c.CreationTime <= Convert.ToDateTime(daterange.Split('~')[1].Trim()).AddDays(1))
                    .AndIF(!string.IsNullOrEmpty(condition), c => c.LogFunction.Contains(condition) || c.LogContent.Contains(condition));
                var logs = await _logServices.Query(expressionable.ToExpression()).ConfigureAwait(false);
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