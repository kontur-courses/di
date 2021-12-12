using System.Drawing;

namespace TagsCloudVisualization.Common.Settings
{
    public class CanvasSettings : ICanvasSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackgroundColor { get; set; }

        public CanvasSettings()
        {
            Width = 0;
            Height = 0;
            BackgroundColor = Color.DimGray;
        }

        public CanvasSettings(int width, int height, Color backgroundColor)
        {
            Width = width;
            Height = height;
            BackgroundColor = backgroundColor;
        }
    }
}