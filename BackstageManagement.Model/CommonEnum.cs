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
    public enum PermissionType { 
        /// <summary>
        /// 菜单
        /// </summary>
        Menu=1,
        /// <summary>
        /// 按钮
        /// </summary>
        Button=2,
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
    }
}