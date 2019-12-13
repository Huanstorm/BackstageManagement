using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace BackstageManagement.Model.Models
{
    [SugarTable("InfoConfig")]
    public class InfoConfigEntity:RootEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(Length =50)]
        public string KeyName { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [SugarColumn(Length = 50)]
        public string Value { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(Length = 255, IsNullable = true)]
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        [SugarColumn(IsIgnore=true)]
        public string CreationTimeString { get { return CreationTime.ToString("yyyy-MM-dd HH:mm"); } }


    }
}