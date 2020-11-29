using System;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public class TextColor : ICloudParameter
    {
        public ParameterType Type => ParameterType.TextColor;
        public Func<string, object> GetValue => ColorFromString.GetColorFromString;
    }
}