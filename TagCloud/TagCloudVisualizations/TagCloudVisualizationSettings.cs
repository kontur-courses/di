using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.TagCloudVisualizations
{
    public class TagCloudVisualizationSettings : ITagCloudVisualizationSettings
    {
        public ImageFormat PictureFormat { get; set; }
        public Size? PictureSize { get; set; }

        public Color? TextColor { get; set; }
        public string FontFamilyName { get; set; }

        public static TagCloudVisualizationSettings Default()
        {
            var settings = new TagCloudVisualizationSettings
            {
                PictureFormat = ImageFormat.Png,
                PictureSize = new Size(1000, 1000),
                TextColor = null,
                FontFamilyName = "Arial"
            };
            return settings;
        }
    }
}
