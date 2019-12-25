using System;
using System.Collections.Generic;
using SqlSugar;

namespace BackstageManagement.Model.Models  
{
    [SugarTable("SystemUser")]
    public class SystemUserEntity:RootEntity
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [SugarColumn(Length =50,IsNullable =true)]
        public string LoginName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string RealName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [SugarColumn(Length = 200,IsNullable =true)]
        public string Password { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? RoleId { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int CreateUserId { get; set; }
        /// <summary>
        /// 修改用户ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? ModifyUserId { get; set; }

    }
}