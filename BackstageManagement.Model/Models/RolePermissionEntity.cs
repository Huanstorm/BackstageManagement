using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace BackstageManagement.Model.Models
{
    [SugarTable("RolePermission")]
    public class RolePermissionEntity:RootEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int RoleId { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int PermissionId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public PermissionEntity Permission { get; set; }
        [SugarColumn(IsIgnore = true)]
        public RoleEntity Role { get; set; }

    }
}
