using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TagsCloud.Contracts;
using TagsCloud.Conveyors;
using TagsCloud.Filters;
using TagsCloud.Processors;

namespace TagsCloud.Extensions;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection AddFilters(this ServiceCollection collection)
    {
        var filters = Assembly
                      .GetExecutingAssembly()
                      .GetTypes()
                      .Where(type => type.IsSubclassOf(typeof(FilterBase)));

        foreach (var filterType in filters)
            collection.AddSingleton(typeof(FilterBase), filterType);

        collection.AddSingleton<FilterConveyor>();

        return collection;
    }

    public static ServiceCollection AddProcessors(this ServiceCollection collection)
    {
        collection.AddSingleton<InputProcessor>();
        collection.AddSingleton<CloudProcessor>();
        collection.AddSingleton<OutputProcessor>();

        return collection;
    }

    public static ServiceCollection AddReaders(this ServiceCollection collection)
    {
        var readers = Assembly
                      .GetExecutingAssembly()
                      .GetTypes()
                      .Where(type => type.GetInterfaces().Any(inter => inter == typeof(IFileReader)));

        foreach (var reader in readers)
            collection.AddSingleton(typeof(IFileReader), reader);

        return collection;
    }

    public static ServiceCollection AddPainters(this ServiceCollection collection)
    {
        var painters = Assembly
                       .GetExecutingAssembly()
                       .GetTypes()
                       .Where(type => type.GetInterfaces().Any(inter => inter == typeof(IPainter)));

        foreach (var painter in painters)
            collection.AddSingleton(typeof(IPainter), painter);

        return collection;
    }
}