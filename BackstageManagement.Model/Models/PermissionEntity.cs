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
        /// 父级菜单名称
        /// </summary>
        [SugarColumn(IsIgnore=true)]
        public string ParentName { get; set; }
        /// <summary>
        /// 父级菜单
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public PermissionEntity Parent { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [SugarColumn(Length = 50)]
        public string PermissionName { get; set; }
        /// <summary>
        /// 菜单地址
        /// </summary>
        [SugarColumn(IsNullable =true, Length = 50)]
        public string PermissionUrl { get; set; }
        /// <summary>
        /// 菜单描述
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 255)]
        public string PermissionDescription { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
        [SugarColumn(IsIgnore=true)]
        public string ModifyTimeString { get { return ModifyTime.ToString("yyyy-MM-dd HH:mm");} }
        /// <summary>
        /// 是否选中
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool IsChecked { get; set; } = false;

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length =255,IsNullable =true)]
        public string Remark { get; set; }
    }
}