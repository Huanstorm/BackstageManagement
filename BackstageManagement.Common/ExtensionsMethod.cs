using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}