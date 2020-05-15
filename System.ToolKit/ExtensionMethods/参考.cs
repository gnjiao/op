using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.ToolKit
{
    #region whereif
    /// <summary>
    /// c# 扩展方法奇思妙用基础篇 六：WhereIf 扩展 http://www.cnblogs.com/ldp615/archive/2011/02/17/WhereIf-ExtensionMethod.html
    /// </summary>
    public static class WhereIfExtension
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, int, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, int, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
    #endregion

    #region Distinct 扩展
    public class CommonEqualityComparer<T, V> : IEqualityComparer<T>
    {
        private Func<T, V> keySelector;
        private IEqualityComparer<V> comparer;

        public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public CommonEqualityComparer(Func<T, V> keySelector)
            : this(keySelector, EqualityComparer<V>.Default)
        { }

        public bool Equals(T x, T y)
        {
            return comparer.Equals(keySelector(x), keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return comparer.GetHashCode(keySelector(obj));
        }
    }
    public static class DistinctExtensions
    {
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }

        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector, comparer));
        }

        ////举例:
        //var data3 = new Person[] {
        //    new Person{ ID = 1, Name = "LDP"},
        //    new Person{ ID = 2, Name = "ldp"}
        //};
        //var ps3 = data3
        //    .Distinct(p => p.Name, StringComparer.CurrentCultureIgnoreCase)
        //    .ToArray();

        }

    #endregion

    #region Expression 扩展
    public static class ExpressionExtensions
    {
        public static Expression AndAlso(this Expression left, Expression right)
        {
            return Expression.AndAlso(left, right);
        }
        public static Expression Call(this Expression instance, string methodName, params Expression[] arguments)
        {
            return Expression.Call(instance, instance.Type.GetMethod(methodName), arguments);
        }
        public static Expression Property(this Expression expression, string propertyName)
        {
            return Expression.Property(expression, propertyName);
        }
        public static Expression GreaterThan(this Expression left, Expression right)
        {
            return Expression.GreaterThan(left, right);
        }
        public static Expression<TDelegate> ToLambda<TDelegate>(this Expression body, params ParameterExpression[] parameters)
        {
            return Expression.Lambda<TDelegate>(body, parameters);
        }
    }
    #endregion

    #region In
    ////ScottGu In扩展 改进
    //public static class InExtensions
    //{
    //    public static bool In<T>(this T t, params T[] c)
    //    {
    //        return c.Any(i => i.Equals(t));
    //    }
    //    public static bool In(this object o, IEnumerable c)
    //    {
    //        foreach (object i in c)
    //        {
    //            if (i.Equals(o)) return true;
    //        }
    //        return false;
    //    }
    //}
    //////举例:
    ////bool b1 = 1.In(1, 2, 3, 4, 5);
    ////bool b2 = 1.In(1, 2, 3, 4, 5, 5, 7, 8);
    ////bool b3 = "Tom".In("Bob", "Kitty", "Tom");
    ////bool b4 = "Tom".In("Bob", "Kitty", "Tom", "Jim");

    
    #endregion
}
