using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;

namespace TagsCloudContainer.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrIncreaseCount<TKey>(this Dictionary<TKey, int> dict, TKey key)
        {
            if (dict.ContainsKey(key))
                dict[key] += 1;
            else
                dict[key] = 1;
        }

        public static IEnumerable<Parameter> ToContainerParameters(this Dictionary<string, object> dict)
            =>dict.Select(parameter => new NamedParameter(parameter.Key, parameter.Value))
                .Cast<Parameter>()
                .ToList();
    }
}