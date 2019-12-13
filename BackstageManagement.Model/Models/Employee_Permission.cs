using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackstageManagement.Model.Models
{
    public class Employee_Permission:RootEntity
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [SugarColumn(Length =50)]
        public int EmployeeId { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public int PermissionId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        [SugarColumn(IsIgnore=true)]
        public PermissionEntity Permission { get; set; }
    }
}