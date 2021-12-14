using System.Drawing;
using TagsCloudVisualization.Commands;

namespace TagsCloudVisualization.Common.Settings
{
    public class CanvasSettings : ICanvasSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackgroundColor { get; set; }

        public CanvasSettings(int width, int height, Color backgroundColor)
        {
            Width = width;
            Height = height;
            BackgroundColor = backgroundColor;
        }

        public CanvasSettings(CommandLineOptions options)
        {
            Width = options.Width;
            Height = options.Height;
            BackgroundColor = ColorTranslator.FromHtml(options.BackgroundColor.Trim());
        }
    }
}