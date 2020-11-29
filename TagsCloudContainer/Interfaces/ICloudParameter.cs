using System;
using TagsCloudContainer.Enums;

namespace TagsCloudContainer.Interfaces
{
    public interface ICloudParameter
    {
        ParameterType Type { get; }
        Func<string, object> GetValue { get; }
    }
}