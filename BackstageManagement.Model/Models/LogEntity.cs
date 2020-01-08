using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackstageManagement.Model.Models
{
    [SugarTable("Log")]
    public class LogEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true, IsNullable = false)]
        public int Id { get; set; }
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
        /// 登录ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [SugarColumn(IsIgnore =true)]
        public string LoginName { get; set; }
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
        /// <summary>
        /// Ip地址
        /// </summary>
        [SugarColumn(Length =16,ColumnDescription ="Ip地址")]
        public string Ip { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        [SugarColumn(Length = 50, ColumnDescription = "城市地址")]
        public string CityName { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string CreationTimeString { get { return CreationTime.ToString("yyyy-MM-dd HH:mm"); } }
    }
}