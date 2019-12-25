using System;
using System.Collections.Generic;
using SqlSugar;

namespace BackstageManagement.Model.Models  
{
    [SugarTable("Employee")]
    public class EmployeeEntity:RootEntity
    {
        /// <summary>
        /// 工号
        /// </summary>
        [SugarColumn(Length =50)]
        public string LoginNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [SugarColumn(Length = 50)]
        public string LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [SugarColumn(Length = 50,IsNullable =true)]
        public string Password { get; set; }
        /// <summary>
        /// 员工类型（0代表管理人员，1代表维护人员，2代表测试人员）
        /// </summary>
        public EmployeeType EmployeeType { get; set; }
        [SugarColumn(IsIgnore=true)]
        public string EmployeeTypeString { get { return EmployeeType.ToString(); } }
        /// <summary>
        /// 信息修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        [SugarColumn(IsIgnore=true)]
        public string ModifyTimeString { get { return ModifyTime.ToString("yyyy-MM-dd HH:mm"); } }
        
        [SugarColumn(IsIgnore=true)]
        public List<Employee_Permission> Employee_Permissions { get; set; }
        [SugarColumn(IsIgnore = true)]
        public List<PermissionEntity> Permissions { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 255,IsNullable =true)]
        public string Remark { get; set; }

    }
}