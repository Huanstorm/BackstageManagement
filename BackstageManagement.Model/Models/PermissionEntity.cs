using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace BackstageManagement.Model.Models
{
    [SugarTable("Permission")]
    public class PermissionEntity:RootEntity
    {
        /// <summary>
        /// 父级菜单Id
        /// </summary>
        [SugarColumn(IsNullable =true)]
        public int? ParentId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        [SugarColumn(Length = 100)]
        public string Name { get; set; }
        /// <summary>
        /// 菜单地址
        /// </summary>
        [SugarColumn(IsNullable =true, Length = 50)]
        public string Url { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Icon { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType Type { get; set; }       

        /// <summary>
        /// 是否选中
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool IsChecked { get; set; } = false;
        /// <summary>
        /// 父级菜单名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string ParentName { get; set; }
        /// <summary>
        /// 父级菜单
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public PermissionEntity Parent { get; set; }
    }
}