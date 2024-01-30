using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using TagsCloud.CustomAttributes;

namespace TagsCloud.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly Type[] assemblyTypes;

    static ServiceCollectionExtensions()
    {
        assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
    }

    public static ServiceCollection AddAllInjections(this ServiceCollection collection)
    {
        var types = assemblyTypes
            .Where(type => Attribute.IsDefined(type, typeof(InjectionAttribute)));

        foreach (var implementationType in types)
        {
            var attribute = implementationType.GetCustomAttribute<InjectionAttribute>();
            var serviceType = implementationType.GetInterfaces().First();
            collection.Add(new ServiceDescriptor(serviceType, implementationType, attribute!.LifeTime));
        }

        return collection;
    }
}