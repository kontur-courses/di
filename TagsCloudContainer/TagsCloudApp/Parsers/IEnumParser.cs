using System;

namespace TagsCloudApp.Parsers
{
    public interface IEnumParser
    {
        T Parse<T>(string value) where T : struct, Enum;
    }
}