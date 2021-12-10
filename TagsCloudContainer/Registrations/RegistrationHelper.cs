using Autofac;
using System.Reflection;

namespace TagsCloudContainer.Registrations;

public static class RegistrationHelper
{
    public static void RegisterServices(ContainerBuilder builder, Assembly[] assemblies)
    {
        builder.RegisterAssemblyTypes(assemblies).AsSelf().AsImplementedInterfaces();
    }
}
