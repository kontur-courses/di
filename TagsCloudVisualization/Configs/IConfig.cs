using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IConfig
    {
        Font Font { get; }
        Point Center { get; }

        Color TextColor { get; }

        public void SetValues(Font font, Point center, Color textColor);
    }
}