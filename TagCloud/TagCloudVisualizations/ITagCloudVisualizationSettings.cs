using System.Drawing;

namespace TagCloud.TagCloudVisualizations
{
    public interface ITagCloudVisualizationSettings
    {
        public Size? PictureSize { get; set; }

        public Color BackgroundColor { get; set; }
        public Color? TextColor { get; set; }
        public int TextScale { get; set; }
        public string FontFamilyName { get; set; }
    }
}
