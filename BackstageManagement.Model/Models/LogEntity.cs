using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackstageManagement.Model.Models
{
    [SugarTable("Log")]
    public class LogEntity:RootEntity
    {
        /// <summary>
        /// 所属系统
        /// </summary>
        
        public BelongSystem BelongSystem { get; set; }
        /// <summary>
        /// 所属系统名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string BelongSystemString { get { return BelongSystem.ToString(); } }
        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType LogType { get; set; }
        /// <summary>
        /// 日志类型名称
        /// </summary>
        [SugarColumn(IsIgnore=true)]
        public string LogTypeString { get { return LogType.ToString(); } }
        /// <summary>
        /// 登录名
        /// </summary>
        public int LoginId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [SugarColumn(IsIgnore =true)]
        public string LoginNo { get; set; }
        /// <summary>
        /// 日志功能
        /// </summary>
        [SugarColumn(Length =100)]
        public string LogFunction { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        [SugarColumn(Length = 8000)]
        public string LogContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;
        [SugarColumn(IsIgnore = true)]
        public string CreationTimeString { get { return CreationTime.ToString("yyyy-MM-dd HH:mm"); } }
    }
}