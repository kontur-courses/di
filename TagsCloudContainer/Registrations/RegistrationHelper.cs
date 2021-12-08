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
                .Where(
                    method => method.GetCustomAttribute<RegisterAttribute>() != null && method.IsStatic
                    )
                 )
             );

        foreach (var method in methods)
        {
            method.Invoke(null, new[] { builder });
        }
    }
}
