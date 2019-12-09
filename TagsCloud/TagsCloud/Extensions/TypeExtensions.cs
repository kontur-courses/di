using System;
using System.Linq;

namespace TagsCloudGenerator.Extensions
{
    internal static class TypeExtensions
    {
        public static TAttributeObj GetFirstAttributeObj<TAttributeObj>(this Type type) =>
            type
            .GetCustomAttributes(typeof(TAttributeObj), false)
            .Cast<TAttributeObj>()
            .First();
    }
}