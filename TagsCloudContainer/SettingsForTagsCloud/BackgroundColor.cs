using System;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public class BackgroundColor : ICloudParameter
    {
        public ParameterType Type => ParameterType.BackgroundColor;
        public Func<string, object> GetValue => ColorFromString.GetColorFromString;
    }
}