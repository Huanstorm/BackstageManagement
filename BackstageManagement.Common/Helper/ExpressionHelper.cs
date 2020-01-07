using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.Common
{
    public class ExpressionHelper
    {
        public static Expression<Func<T, bool>> True<T>() => c => true;

        public static Expression<Func<T, bool>> False<T>() => c => false;

        public static SqlSugar.Expressionable<T> Expressionable<T>() where T : class, new() => SqlSugar.Expressionable.Create<T>();
    
    }
}
