using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace TagCloud.TagCloudVisualizations
{
    public class TagCloudBitmapVisualizationSettings : ITagCloudVisualizationSettings
    {
        public ImageFormat Format { get; set; }
        public Color? TextColor { get; set; }

        public Size? PictureSize { get; set; }

        public static TagCloudBitmapVisualizationSettings Default()
        {
            var settings = new TagCloudBitmapVisualizationSettings();
            settings.Format = ImageFormat.Png;
            settings.TextColor = null;
            settings.PictureSize = null;
            return settings;
        }
    }
}
