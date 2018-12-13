using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
namespace Extensions
{
    public static class IEnumerableExtensions
    {
        public static T TakeOnlyOne<T>(this IEnumerable<T> enumerable, string emptyErrorMessage = null, string owerflowErrorMessage = null)
        {
            T tmp;
            using (var enumerator = enumerable.GetEnumerator())
            {
                if(!enumerator.MoveNext())
                    throw new ArgumentException(emptyErrorMessage ?? "Enumeration was empty");
                tmp = enumerator.Current;
                if(enumerator.MoveNext())
                    throw new ArgumentException(owerflowErrorMessage ?? "Enumeration contains more than one element.");   
            }
            return tmp;
        }

        public static IEnumerable<TOut> ForAllPairs<TIn, TOut>(this IList<TIn> list, Func<TIn, TIn, TOut> func) =>
            list.AllPairs().Select(x => func(x.Item1, x.Item2));
        
        public static void ForAllPairs<TIn>(this IList<TIn> list, Action<TIn,TIn> func)=>
            list.AllPairs().Foreach(x => func(x.Item1, x.Item2));
        
        public static IEnumerable<(TIn,TIn)> AllPairs<TIn>(this IList<TIn> list)
        {
            for (int i = 0; i < list.Count; i++)
            for (int j = 0; j < i; j++)
                yield return ValueTuple.Create(list[i], list[j]);
        }
                
        public static void ForAllPairs<TIn>(this IEnumerable<TIn> enumerable, Action<TIn,TIn> func)
        {
            using (var enumerator = enumerable.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return;
                var list = new List<TIn>{enumerator.Current};
                while (enumerator.MoveNext())
                {
                    foreach (var el in list)
                        func(enumerator.Current, el);
                    list.Add(enumerator.Current);
                }
            }
        }

        public static Rectangle EnclosingRectangle(this IEnumerable<Rectangle> enumerable) =>
            enumerable.Aggregate((x, y) =>
            {
                var left = Math.Min(x.Left, y.Left);
                var top = Math.Min(x.Top, y.Top);
                var right = Math.Max(x.Right, y.Right);
                var bottom = Math.Max(x.Bottom, y.Bottom);
                return new Rectangle(left,top,right-left,bottom-top);
            });
        
        public static void Times(this int count, Action act)
        {
            for (int i = 0; i < count; i++)
                act();
        }
        
        public static IEnumerable<T> Times<T>(this int count, Func<T> act)
        {
            for (int i = 0; i < count; i++)
                yield return act();
        }

        public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> act)
        {
            foreach (var el in enumerable)
                act(el);
        }

        public static IEnumerable<T[]> SplitBy<T>(this IEnumerable<T> enumerable,Func<T,bool> predicate, bool removeEmpty = true)
        {
            var list = new List<T>();
            foreach (var el in enumerable)
                if (!predicate(el))
                    list.Add(el);
                else
                {
                    if (list.Count == 0 && removeEmpty) continue;
                    yield return list.ToArray();
                    list.Clear();
                }
        }
    }
}