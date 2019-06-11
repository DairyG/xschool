using System;
using System.Collections.Generic;

namespace XSchool.Helpers
{
    /// <summary>
    /// 创建相等判断接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Equality<T>
    {
        /// <summary>
        /// 返回一个对比接口
        /// </summary>
        /// <param name="func">用于判断两个值是否相等</param>
        /// <returns></returns>
        public static IEqualityComparer<T> Comparer(Func<T, T, bool> func)
        {
            return new GeneralEqualityComparer<T>((t, t1) => { return func(t, t1); });
        }

        /// <summary>
        /// 返回一个对比接口
        /// </summary>
        /// <typeparam name="V">对比的值类型</typeparam>
        /// <param name="func">对比的值</param>
        /// <returns></returns>
        public static IEqualityComparer<T> Comparer<V>(Func<T, V> func)
        {
            return new GeneralEqualityComparer<T>((t, t1) => { return func(t).Equals(func(t1)); });
        }

        /// <summary>
        /// 返回一个对比接口
        /// </summary>
        /// <typeparam name="V">对比的值类型1</typeparam>
        /// <typeparam name="V1">对比的值类型2</typeparam>
        /// <param name="func">对比的值1</param>
        /// <param name="func1">对比的值2</param>
        /// <returns></returns>
        public static IEqualityComparer<T> Comparer<V, V1>(Func<T, V> func, Func<T, V1> func1)
        {
            return new GeneralEqualityComparer<T>((t, t1) => { return func(t).Equals(func(t1)) && func1(t).Equals(func1(t1)); });
        }

        /// <summary>
        /// 返回一个对比接口
        /// </summary>
        /// <typeparam name="V">对比的值类型1</typeparam>
        /// <typeparam name="V1">对比的值类型2</typeparam>
        /// <typeparam name="V2">对比的值类型3</typeparam>
        /// <param name="func">对比的值1</param>
        /// <param name="func1">对比的值2</param>
        /// <param name="func2">对比的值3</param>
        /// <returns></returns>
        public static IEqualityComparer<T> Comparer<V, V1, V2>(Func<T, V> func, Func<T, V1> func1, Func<T, V2> func2)
        {
            return new GeneralEqualityComparer<T>((t, t1) => { return func(t).Equals(func(t1)) && func1(t).Equals(func1(t1)) && func2(t).Equals(func2(t1)); });
        }

        /// <summary>
        /// 返回一个对比接口
        /// </summary>
        /// <typeparam name="V">对比的值类型</typeparam>
        /// <param name="func">对比的值</param>
        /// <param name="comparer">使用的比较器，比如string的不区分大小写</param>
        /// <returns></returns>
        public static IEqualityComparer<T> Comparer<V>(Func<T, V> func, IEqualityComparer<V> comparer)
        {
            return new GeneralEqualityComparer<T>((t, t1) => { return comparer.Equals(func(t), func(t1)); });
        }

        /// <summary>
        /// 返回一个对比接口
        /// </summary>
        /// <typeparam name="V">对比的值类型1</typeparam>
        /// <typeparam name="V1">对比的值类型2</typeparam>
        /// <param name="func">对比的值1</param>
        /// <param name="func1">对比的值2</param>
        /// <param name="comparer">使用的比较器，比如string的不区分大小写</param>
        /// <returns></returns>
        public static IEqualityComparer<T> Comparer<V, V1>(Func<T, V> func, Func<T, V> func1, IEqualityComparer<V> comparer)
        {
            return new GeneralEqualityComparer<T>((t, t1) => { return comparer.Equals(func(t), func(t1)) && comparer.Equals(func1(t), func1(t1)); });
        }

        /// <summary>
        /// 返回一个对比接口
        /// </summary>
        /// <typeparam name="V">对比的值类型1</typeparam>
        /// <typeparam name="V1">对比的值类型2</typeparam>
        /// <typeparam name="V2">对比的值类型3</typeparam>
        /// <param name="func">对比的值1</param>
        /// <param name="func1">对比的值2</param>
        /// <param name="func2">对比的值3</param>
        /// <param name="comparer">使用的比较器，比如string的不区分大小写</param>
        /// <returns></returns>
        public static IEqualityComparer<T> Comparer<V, V1, V2>(Func<T, V> func, Func<T, V> func1, Func<T, V> func2, IEqualityComparer<V> comparer)
        {
            return new GeneralEqualityComparer<T>((t, t1) => { return comparer.Equals(func(t), func(t1)) && comparer.Equals(func1(t), func1(t1)) && comparer.Equals(func2(t), func2(t1)); });
        }
    }

    /// <summary>
    /// 通用的对比接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GeneralEqualityComparer<T> : IEqualityComparer<T>
    {
        private Func<T, T, bool> _equailty = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equailty">对比的具体函数</param>
        public GeneralEqualityComparer(Func<T, T, bool> equailty)
        {
            _equailty = equailty;
        }


        /// <summary>
        /// 判断是否相等
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(T x, T y)
        {
            if (_equailty != null)
                return _equailty(x, y);
            return x.Equals(y);
        }

        /// <summary>
        /// HashCode
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(T obj)
        {
            if (_equailty != null)
                return _equailty.GetHashCode();
            return 0;
        }
    }

    /// <summary>
    /// 比较辅助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Comparer<T>
    {
        /// <summary>
        /// 返回一个对比接口
        /// </summary>
        /// <param name="func">用于判断两个值是否相等 指示 x 与 y 的相对值，如下表所示。 值 含义 小于零 x 小于 y。 零 x 等于 y。 大于零 x 大于 y。</param>
        /// <returns></returns>
        public static IComparer<T> Create(Func<T, T, int> func)
        {
            return new GeneralComparer<T>((t, t1) => { return func(t, t1); });
        }

        /// <summary>
        /// 返回一个对比接口
        /// </summary>
        /// <typeparam name="V">对比的值类型</typeparam>
        /// <param name="func">对比的值 指示 x 与 y 的相对值，如下表所示。 值 含义 小于零 x 小于 y。 零 x 等于 y。 大于零 x 大于 y。</param>
        /// <returns></returns>
        public static IComparer<T> Create<V>(Func<T, V> func) where V : IComparable<V>
        {
            return new GeneralComparer<T>((t, t1) => { return func(t).CompareTo(func(t1)); });
        }

        /// <summary>
        /// 返回一个对比接口
        /// </summary>
        /// <typeparam name="V">对比的值类型</typeparam>
        /// <param name="func">对比的值 指示 x 与 y 的相对值，如下表所示。 值 含义 小于零 x 小于 y。 零 x 等于 y。 大于零 x 大于 y。</param>
        /// <returns></returns>
        public static IComparer<T> CreateDesc<V>(Func<T, V> func) where V : IComparable<V>
        {
            return new GeneralComparer<T>((t, t1) => { return func(t1).CompareTo(func(t)); });
        }

        /// <summary>
        /// 返回一个对比接口
        /// </summary>
        /// <typeparam name="V">对比的值类型1 对比的值 指示 x 与 y 的相对值，如下表所示。 值 含义 小于零 x 小于 y。 零 x 等于 y。 大于零 x 大于 y。</typeparam>
        /// <typeparam name="V1">对比的值类型2</typeparam>
        /// <param name="func">对比的值1</param>
        /// <param name="func1">对比的值2</param>
        /// <returns></returns>
        public static IComparer<T> Create<V, V1>(Func<T, V> func, Func<T, V1> func1)
            where V : IComparable
            where V1 : IComparable
        {
            return new GeneralComparer<T>((t, t1) => { return func(t).CompareTo(func(t1)).CompareTo(func1(t).CompareTo(func1(t1))); });
        }

        /// <summary>
        /// 返回一个对比接口
        /// </summary>
        /// <typeparam name="V">对比的值类型1 对比的值 指示 x 与 y 的相对值，如下表所示。 值 含义 小于零 x 小于 y。 零 x 等于 y。 大于零 x 大于 y。</typeparam>
        /// <typeparam name="V1">对比的值类型2</typeparam>
        /// <param name="func">对比的值1</param>
        /// <param name="func1">对比的值2</param>
        /// <returns></returns>
        public static IComparer<T> CreateDesc<V, V1>(Func<T, V> func, Func<T, V1> func1)
            where V : IComparable
            where V1 : IComparable
        {
            return new GeneralComparer<T>((t, t1) => { return func(t1).CompareTo(func(t)).CompareTo(func1(t1).CompareTo(func1(t))); });
        }
    }

    /// <summary>
    /// 通用比较器 用于排序
    /// </summary>
    /// <typeparam name="T">泛型T</typeparam>
    public class GeneralComparer<T> : IComparer<T>
    {
        private Func<T, T, int> _equailty = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="equailty">对比的具体函数 指示 x 与 y 的相对值，如下表所示。 值 含义 小于零 x 小于 y。 零 x 等于 y。 大于零 x 大于 y。</param>
        public GeneralComparer(Func<T, T, int> equailty)
        {
            _equailty = equailty;
        }

        /// <summary>
        /// 比较两个对象
        /// </summary>
        /// <param name="x">要比较的X</param>
        /// <param name="y"></param>
        /// <returns>比较结果小于零x小于y,等于零x等于y,大于零x大于y</returns>
        public int Compare(T x, T y)
        {
            if (_equailty != null)
                return _equailty(x, y);
            return 0;
        }
    }

    public class TypeComparable : IComparable<Type>
    {
        private readonly Type _type;
        public TypeComparable(Type type)
        {
            this._type = type;
        }

        private void Compare(Type type, ref int value)
        {
            if (type == typeof(object))
            {
                return;
            }
            else if (type == _type)
            {
                value = 1;
            }
            else if (type.IsGenericType)
            {
                Compare(type.GetGenericTypeDefinition(), ref value);
                if (value != 1)
                {
                    Compare(type.BaseType, ref value);
                }
            }
            else
            {
                if (type.BaseType != null)
                {
                    Compare(type.BaseType, ref value);
                }
            }
            return;
        }

        public int CompareTo(Type other)
        {
            if (other == _type) return 0;
            var value = -1;
            Compare(other.BaseType, ref value);
            return value;
        }
    }

}
