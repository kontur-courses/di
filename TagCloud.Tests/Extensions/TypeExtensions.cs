using System.Reflection;

namespace TagCloud.Tests.Extensions;

public static class TypeExtensions
{
    public static object? GetAndInvokeMethod(this Type type, string methodName, BindingFlags flags, object obj, params object[] parameters)
    {
        return type
            .GetMethod(methodName, flags)!
            .Invoke(obj, parameters);
    }
}