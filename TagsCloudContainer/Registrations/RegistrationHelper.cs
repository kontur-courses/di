using Autofac;
using System.Reflection;

namespace TagsCloudContainer.Registrations;

public static class RegistrationHelper
{
    public static void RegisterServices(ContainerBuilder builder, Assembly[] assemblies)
    {
        var methods = assemblies.SelectMany(
            assembly => assembly.GetTypes()
            .SelectMany(
                type => type.GetMethods()
                .Where(method => method.IsStatic)
                 )
             );

        foreach (var method in methods)
        {
            var attr = method.GetCustomAttribute<RegisterAttribute>();
            if (attr == null)
                continue;
            if (attr.IsKeyed)
                method.Invoke(null, new object[] { builder, method.DeclaringType!.Name });
            else
                method.Invoke(null, new object[] { builder });
        }
    }
}
