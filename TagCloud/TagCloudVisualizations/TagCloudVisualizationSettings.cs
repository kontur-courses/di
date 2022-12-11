using System.Drawing;

namespace TagCloud.TagCloudVisualizations
{
    public class TagCloudVisualizationSettings : ITagCloudVisualizationSettings
    {
        public Size? PictureSize { get; set; } = new Size(500, 500);

        public Color BackgroundColor { get; set; } = Color.Black;
        public Color? TextColor { get; set; } = null;
        public int TextScale { get; set; } = 40;
        public string FontFamilyName { get; set; } = "Arial";
    }
}
