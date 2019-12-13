using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackstageManagement.Model
{
    public enum LogType
    {
        /// <summary>
        /// 系统日志
        /// </summary>
        系统日志=1,
        /// <summary>
        /// 异常日志
        /// </summary>
        异常日志=2
    }
    public enum BelongSystem { 
        /// <summary>
        /// 管理系统
        /// </summary>
        管理系统=1,
        /// <summary>
        /// 测试系统
        /// </summary>
        测试系统=2
    }
    public enum EmployeeType
    {
        管理人员 = 1,//管理人员
        测试人员 = 2//测试人员
    }
    public enum WorkStationStatus
    {
        空闲 = 0,
        忙碌 = 1,
        故障 = 2,
    }
    public enum ResponseCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 1,
        /// <summary>
        /// Session过期
        /// </summary>
        SessionOverDue = 2
    }
    public enum IsQualified {
        /// <summary>
        /// 合格
        /// </summary>
        合格=0,
        /// <summary>
        /// 不合格
        /// </summary>
        不合格=1
    }
}