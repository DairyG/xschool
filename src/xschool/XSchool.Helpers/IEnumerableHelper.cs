using System;
using System.Collections.Generic;
using System.Text;

namespace XSchool.Helpers
{
    public static class IEnumerableHelper
    {
        public static IEnumerable<T2> Cast<T, T2>(this IEnumerable<T> source, Func<T, T2> func)
        {
            foreach (var item in source)
            {
                yield return func(item);
            }
        }
    }
}
