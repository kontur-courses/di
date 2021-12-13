using System;

namespace TagsCloudContainer.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsInstanceOf<TInterface>(this Type type)
        {
            return typeof(TInterface).IsAssignableFrom(type)
                   && !(type.IsAbstract || type.IsInterface);
        }
    }
}