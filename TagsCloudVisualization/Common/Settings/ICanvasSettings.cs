using System.Drawing;

namespace TagsCloudVisualization.Common.Settings
{
    public interface ICanvasSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color BackgroundColor { get; set; }
    }
}