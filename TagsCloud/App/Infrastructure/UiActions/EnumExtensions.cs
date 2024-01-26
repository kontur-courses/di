using System.ComponentModel;

namespace TagsCloud.Infrastructure.UiActions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum enumValue)
    {
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
        var description = fieldInfo
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .Cast<DescriptionAttribute>()
            .FirstOrDefault()?.Description;

        return description ?? enumValue.ToString();
    }
}