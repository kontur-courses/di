using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TagsCloud.Contracts;
using TagsCloud.Entities;

namespace TagsCloud.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddFilters(this ServiceCollection collection)
    {
        var filters = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsClass)
            .Where(type => type.GetInterfaces().Any(inter => inter == typeof(IWordFilter)));

        foreach (var filterType in filters)
            collection.AddSingleton(typeof(IWordFilter), filterType);
    }

    public static void AddConveyor(this ServiceCollection collection)
    {
        collection.AddSingleton<FilterConveyor>();
    }

    public static void AddFilterOptions(this ServiceCollection collection, FilterOptions options)
    {
        collection.AddSingleton(options);
    }
}