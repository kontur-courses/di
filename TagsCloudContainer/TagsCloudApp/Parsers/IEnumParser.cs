using System;

namespace TagsCloudApp.Parsers
{
    public interface IEnumParser
    {
        T Parse<T>(string value) where T : struct, Enum;
    }

    // public class EnumParser : IEnumParser
    // {
    //     public T Parse<T>(string value) where T : struct, Enum
    //     {
    //         if (Enum.TryParse<T>(value, true, out var enumValue))
    //             return enumValue;
    //
    //         var availableValue = string.Join(", ", Enum.GetNames<T>());
    //         throw new ApplicationException($"Invalid value: {value}. Available values: {availableValue}");
    //     }
    // }
}