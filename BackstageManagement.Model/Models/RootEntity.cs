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
    }
}
