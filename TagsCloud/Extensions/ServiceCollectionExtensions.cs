using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TagsCloud.Conveyors;
using TagsCloud.Entities;
using TagsCloud.Filters;

namespace TagsCloud.Extensions;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection AddFiltersWithOptions(this ServiceCollection collection, FilterOptions options)
    {
        var filters = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsClass)
            .Where(type => type.IsSubclassOf(typeof(FilterBase)));

        foreach (var filterType in filters)
            collection.AddSingleton(typeof(FilterBase), filterType);

        collection.AddSingleton<FilterConveyor>();
        collection.AddSingleton(options);

        return collection;
    }
}