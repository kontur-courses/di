using System;
using System.Drawing;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public class ImageSize : ICloudParameter
    {
        public ParameterType Type => ParameterType.ImageSize;
        public Func<string, object> GetValue => GetSizeFromString;

        private object GetSizeFromString(string sizeFromString)
        {
            var size = sizeFromString.Split('_');
            if (size.Length != 2 || !int.TryParse(size[0], out var width) || !int.TryParse(size[1], out var height))
                throw new Exception("Invalid format of size");
            return new Size(width, height);
        }
    }
}