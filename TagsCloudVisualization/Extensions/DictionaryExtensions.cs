#region

using System;
using System.Collections;
using System.Linq.Expressions;

#endregion

namespace TagsCloudVisualization.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrUpdate(this IDictionary dictionary, object key, object value,
            Expression<Func<object, object>> func)
        {
            if (dictionary.Contains(key))
                dictionary[key] = func.Compile().Invoke(key);
            else
                dictionary[key] = value;
        }
    }
}