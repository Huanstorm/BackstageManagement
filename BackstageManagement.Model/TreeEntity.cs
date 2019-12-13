using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackstageManagement.Model
{
    /// <summary>
    /// 树形菜单返回统一格式
    /// </summary>
    public class TreeEntity {
        /// <summary>
        /// 节点标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 节点唯一索引值
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 节点字段名
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 点击节点弹出新窗口对应的url
        /// </summary>
        public string href { get; set; }
        /// <summary>
        /// 节点是否初始展开
        /// </summary>
        public bool spread { get; set; } = true;
        /// <summary>
        /// 节点是否初始为选中状态
        /// </summary>
        public bool @checked { get; set; } = false;
        /// <summary>
        /// 节点是否为禁用状态
        /// </summary>
        public bool disabled { get; set; } = false;
        private List<TreeEntity> _children;
        /// <summary>
        /// 子节点
        /// </summary>
        public List<TreeEntity> children
        {
            get {
                if (_children == null) {
                    _children = new List<TreeEntity>();
                }
                return _children;
            }
            set { _children = value; }
        }
        
    }
}