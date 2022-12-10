using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.TagCloudVisualizations
{
    public class TagCloudVisualizationSettings : ITagCloudVisualizationSettings
    {
        public ImageFormat PictureFormat { get; set; } = ImageFormat.Png;
        public Size? PictureSize { get; set; } = new Size(500, 500);

        public Color BackgroundColor { get; set; } = Color.Black;
        public Color? TextColor { get; set; } = null;
        public string FontFamilyName { get; set; } = "Arial";
    }
}
