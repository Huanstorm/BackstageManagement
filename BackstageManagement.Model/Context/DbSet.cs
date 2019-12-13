using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlSugar;

namespace BackstageManagement.Model.Context
{
    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(SqlSugarClient context) : base(context)
        {

        }
    }
}