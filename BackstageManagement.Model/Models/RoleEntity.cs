using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace BackstageManagement.Model.Models
{
    [SugarTable("Role")]
    public class RoleEntity:RootEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarColumn(Length =50,IsNullable =false)]
        public string Name { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int CreateUserId { get; set; }
        /// <summary>
        /// 修改者ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? ModifyUserId { get; set; }

        
    }
}
