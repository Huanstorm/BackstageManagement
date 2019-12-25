using System;
using SqlSugar;

namespace BackstageManagement.Model.Models
{
    public class RootEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsIdentity =true,IsPrimaryKey =true,IsNullable =false)]
        public int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Remark { get; set; }
        [SugarColumn(IsIgnore = true)]
        public RoleEntity Role { get; set; }
        [SugarColumn(IsIgnore =true)]
        public string CreationTimeString { get { return CreationTime.ToString("yyyy-MM-dd HH:mm"); } }
        [SugarColumn(IsIgnore = true)]
        public string ModifyTimeString { get { return ModifyTime.HasValue? ModifyTime.Value.ToString("yyyy-MM-dd HH:mm"):""; } }
    }
}
