using System.ComponentModel;

namespace TagCloudApp.Infrastructure;

public static class EnumExtensions
{
    public static string GetDescription(this Enum enumValue)
    {
        return enumValue.GetType().GetField(enumValue.ToString())
            ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
            .Cast<DescriptionAttribute>()
            .FirstOrDefault()?.Description ?? enumValue.ToString();
    }
}