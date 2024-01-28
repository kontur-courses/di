using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TagsCloud.Contracts;
using TagsCloud.Conveyors;
using TagsCloud.Processors;

namespace TagsCloud.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly Type[] assemblyTypes;

    static ServiceCollectionExtensions()
    {
        assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
    }

    public static ServiceCollection AddProcessors(this ServiceCollection collection)
    {
        collection.AddSingleton<InputProcessor>();
        collection.AddSingleton<CloudProcessor>();
        collection.AddSingleton<OutputProcessor>();

        return collection;
    }

    public static ServiceCollection AddFilters(this ServiceCollection collection)
    {
        var filterType = typeof(IFilter);
        var filters = GetTypesByInterface(filterType);

        foreach (var filter in filters)
            collection.AddSingleton(filterType, filter);

        collection.AddSingleton<FilterConveyor>();

        return collection;
    }

    public static ServiceCollection AddReaders(this ServiceCollection collection)
    {
        var readerType = typeof(IFileReader);
        var readers = GetTypesByInterface(readerType);

        foreach (var reader in readers)
            collection.AddSingleton(readerType, reader);

        return collection;
    }

    public static ServiceCollection AddPainters(this ServiceCollection collection)
    {
        var painterType = typeof(IPainter);
        var painters = GetTypesByInterface(painterType);

        foreach (var painter in painters)
            collection.AddSingleton(painterType, painter);

        return collection;
    }

    private static IEnumerable<Type> GetTypesByInterface(Type interfaceType)
    {
        return assemblyTypes
            .Where(type => type.GetInterfaces().Any(inter => inter == interfaceType));
    }
}