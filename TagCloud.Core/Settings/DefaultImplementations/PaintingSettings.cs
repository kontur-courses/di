using System.Drawing;
using TagCloud.Core.Settings.Interfaces;

namespace TagCloud.Core.Settings.DefaultImplementations
{
    public class PaintingSettings : IPaintingSettings
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
    }
}