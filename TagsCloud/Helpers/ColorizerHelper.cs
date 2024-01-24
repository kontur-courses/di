using SixLabors.ImageSharp;
using System.Reflection;
using TagsCloud.Colorizers;
using TagsCloud.CustomAttributes;

namespace TagsCloud.Helpers;

public static class ColorizerHelper
{
    public static ColorizerBase? GetAppropriateColorizer(Color[] colors, string colorizerName)
    {
        var colorizerType = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsClass)
            .Where(type => type.IsSubclassOf(typeof(ColorizerBase)))
            .Where(type => Attribute.IsDefined(type, typeof(ColorizerNameAttribute)))
            .FirstOrDefault(type =>
            {
                var actualName = type.GetCustomAttribute<ColorizerNameAttribute>()!.Name;
                return string.Compare(colorizerName, actualName, StringComparison.OrdinalIgnoreCase) == 0;
            });

        if (colorizerType == null)
            return null;

        var ctor = colorizerType.GetConstructor(
            BindingFlags.Public | BindingFlags.Instance,
            new[] { typeof(Color[]) });

        return ctor!.Invoke(new object[] { colors }) as ColorizerBase;
    }
}