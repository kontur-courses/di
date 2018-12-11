using System.Drawing;

namespace TagCloud.Core.Settings
{
    public class PaintingSettings : ISettings
    {
        public Color BackgroundColor { get; set; } = Color.White;
        public Color TagColor { get; set; } = Color.Navy;

        public Brush TagBrush => new SolidBrush(TagColor);

        public string GetSettingsName()
        {
            return "Painting settings";
        }
    }
}