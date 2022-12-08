using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.TagCloudVisualizations
{
    public interface ITagCloudVisualizationSettings
    {
        public ImageFormat PictureFormat { get; set; }
        public Size? PictureSize { get; set; }

        public Color? TextColor { get; set; }
        public string FontFamilyName { get; set; }
    }
}
