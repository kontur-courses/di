using System.Drawing;

namespace TagCloud.Core.Settings
{
    public class PaintingSettings : ISettings
    {
        public PaintingSettings()
        {
            TagColor = Color.Navy;
            BackgroundColor = Color.White;
        }

        public Color BackgroundColor { get; set; }
        public Brush TagBrush { get; private set; }
        private Color tagColor;
        public Color TagColor
        {
            get => tagColor;
            set
            {
                tagColor = value;
                TagBrush = new SolidBrush(value);
            }
        }

        public string GetSettingsName()
        {
            return "Painting settings";
        }
    }
}