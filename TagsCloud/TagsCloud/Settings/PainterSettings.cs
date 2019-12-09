using System.Drawing;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Settings
{
    public class PainterSettings : IPainterSettings
    {
        public PainterSettings() => Reset();

        public Color[] Colors { get; set; }
        public Color BackgroundColor { get; set; }

        public virtual void Reset()
        {
            Colors = new[] { Color.Red, Color.Yellow, Color.Cyan };
            BackgroundColor = Color.Black;
        }
    }
}