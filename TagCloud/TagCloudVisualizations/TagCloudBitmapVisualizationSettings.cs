using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace TagCloud.TagCloudVisualizations
{
    public class TagCloudBitmapVisualizationSettings : ITagCloudVisualizationSettings
    {
        public ImageFormat PictureFormat { get; set; }
        public Size? PictureSize { get; set; }

        public Color? TextColor { get; set; }
        public int MaxTextSize { get; set; }
        public int MinTextSize { get; set; }

        public static TagCloudBitmapVisualizationSettings Default()
        {
            var settings = new TagCloudBitmapVisualizationSettings();
            settings.PictureFormat = ImageFormat.Png;
            settings.TextColor = null;
            settings.PictureSize = null;
            return settings;
        }
    }
}
