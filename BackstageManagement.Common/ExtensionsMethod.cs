using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BackstageManagement
{
    public static class ExtensionsMethod
    {
        /// <summary>
        /// 获取两个集合相同的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="compareSource"></param>
        /// <returns></returns>
        public static IEnumerable<T> RetainAll<T>(this IList<T> source, IList<T> compareSource)
        {
            foreach (var info in source)
            {
                if (compareSource.Contains(info)) 
                    yield return info;
            }
        }

        /// <summary>
        /// 根据另一个对象更新对象的属性值(对象可以不相同)
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="tOne"></param>
        /// <param name="tTwo"></param>
        public static T1 UpdateInfoByClass<T1, T2>(this T1 tOne, T2 tTwo)
        {
            var prosOne = typeof(T1).GetProperties();
            var prosTwo = typeof(T2).GetProperties();
            var proNames = prosOne.Select(c => c.Name).ToList().RetainAll(prosTwo.Select(c => c.Name).ToList()).ToList();
            foreach (var str in proNames)
            {
                var proOne = prosOne.First(c => c.Name == str);
                var proTwo = prosTwo.First(c => c.Name == str);
                if (proOne.CanWrite) proOne.SetValue(tOne, proTwo.GetValue(tTwo), null);
            }
            return tOne;
        }

        /// <summary>
        ///  Or联合两个表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expOne"></param>
        /// <param name="expTwo"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expOne,
         Expression<Func<T, bool>> expTwo)
        {
            var invokedExpression = Expression.Invoke(expTwo, expOne.Parameters
                .Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>(Expression.Or(expOne.Body, invokedExpression),
                expOne.Parameters);
        }

        /// <summary>
        /// And联合两个表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expOne"></param>
        /// <param name="expTwo"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expOne,
            Expression<Func<T, bool>> expTwo)
        {
            var invokedExpression = Expression.Invoke(expTwo, expOne.Parameters
                .Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>(Expression.And(expOne.Body,
                invokedExpression), expOne.Parameters);
        }

        public static IQueryable<T> WhereIF<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> expression) {
            return condition ? source.Where(expression) : source;
        }
    }
}