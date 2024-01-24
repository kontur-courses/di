using SixLabors.ImageSharp;
using System.Reflection;
using TagsCloud.Colorizers;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;

namespace TagsCloud.Helpers;

public static class ColorizerHelper
{
    public static ColorizerBase? GetAppropriateColorizer(Color[] colors, ColoringStrategy strategy)
    {
        var colorizerType = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsClass)
            .Where(type => type.IsSubclassOf(typeof(ColorizerBase)))
            .Where(type => Attribute.IsDefined(type, typeof(ColorizerNameAttribute)))
            .FirstOrDefault(type =>
            {
                var actualStrategy = type.GetCustomAttribute<ColorizerNameAttribute>()!.Strategy;
                return actualStrategy == strategy;
            });

        if (colorizerType == null)
            return null;

        var ctor = colorizerType.GetConstructor(
            BindingFlags.Public | BindingFlags.Instance,
            new[] { typeof(Color[]) });

        return ctor!.Invoke(new object[] { colors }) as ColorizerBase;
    }
}