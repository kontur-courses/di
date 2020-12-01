using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public class CloudImageFormat : ICloudParameter
    {
        private static Dictionary<string, ImageFormat> _imageFormats = new Dictionary<string, ImageFormat>
        {
            ["bmp"] = ImageFormat.Bmp,
            ["jpeg"] = ImageFormat.Jpeg,
            ["png"] = ImageFormat.Png,
            ["gif"] = ImageFormat.Gif,
            ["tiff"] = ImageFormat.Tiff
        };

        public ParameterType Type => ParameterType.CloudImageFormat;
        public Func<string, object> GetValue => GetImageFormatFromString;

        private static object GetImageFormatFromString(string imageFormatFromString)
        {
            if (_imageFormats.TryGetValue(imageFormatFromString.ToLower(), out var format))
                return format;
            throw new Exception("Doesn't contain this image format");
        }
    }
}